using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Mpx.Infrastructure.Data;
using Mpx.Domain.Interfaces;
using Mpx.Application.Handlers;
using mpx.Domain.Interfaces;
using Mpx.Infrastructure.Data.Repositories;
using Renci.SshNet;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Mpx.CrossCutting;

public static class DependencyInjection
{
  public static WebApplicationBuilder AddMorphusDependecyInjection(this WebApplicationBuilder builder)
  {
    AddCors(builder);
    AddMorhusDbContext(builder);
    AddDependencyInjection(builder); // 🧙‍♂️ ADICIONADO: Agora os repositórios e handlers são ativados de verdade!
    AddJsonSettings(builder);
    AddAuth0Authentication(builder); // Chamada única e limpa

    return builder;
  }

  private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
  {
    // 🚀 CONFIGURAÇÃO IMPLACÁVEL DE CORS PARA O .NET 11
    builder.Services.AddCors(options =>
    {
      options.AddPolicy("MorphusCorsPolicy", policy =>
      {
        policy.WithOrigins(
                "http://localhost:4200",  // Seu Angular Local
                "https://localhost:4200", // Seu Angular Local (Se usar HTTPS)
                "https://morphus.site",   // Domínio Oficial do seu Angular na VPS (Ajuste se for outro)
                "http://179.197.232.25"
                ) // Libera a porta do Angular e da VPS
                .AllowAnyHeader()  // 🧙‍♂️ IMPRESCINDÍVEL: Autoriza o cabeçalho Authorization Bearer do Auth0!
                .AllowAnyMethod()  // Autoriza GET, POST, OPTIONS, PUT, DELETE
                .AllowCredentials()
                .SetPreflightMaxAge(TimeSpan.FromMinutes(10)); // Faz o navegador guardar o cache do CORS por 10 minutos (Evita requisições OPTIONS repetidas)
      });
    });

    return builder;
  }

  private static WebApplicationBuilder AddDependencyInjection(this WebApplicationBuilder builder)
  {
    // 🚀 Ajuste de segurança para o UnitOfWork amarrar com o DbContext perfeitamente
    builder.Services.AddScoped<MorphusDbContext>();
    builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<MorphusDbContext>());

    // Registros de Negócio e Repositórios Genéricos
    builder.Services.AddScoped<EmailHandler>();
    builder.Services.AddScoped<ICorporationRepository, CorporationRepository>();
    builder.Services.AddScoped(typeof(IMorphusRepository<>), typeof(MorphusRepository<>));
    builder.Services.AddScoped<IEmailRepository, EmailRepository>();

    return builder;
  }

  private static WebApplicationBuilder AddMorhusDbContext(this WebApplicationBuilder builder)
  {
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    var connectionBuilder = new NpgsqlConnectionStringBuilder(connectionString)
    {
      Password = builder.Configuration["POSTGRES_DB_PASSWORD"] ?? throw new InvalidOperationException("Nenhuma senha de banco de dados configurada")
    };

    builder.Services.AddDbContext<MorphusDbContext>(options =>
             options.UseLazyLoadingProxies()
                    .UseSnakeCaseNamingConvention()
                    .UseNpgsql(connectionBuilder.ConnectionString, b => b
                    .MigrationsAssembly(typeof(MorphusDbContext)
                    .Assembly.FullName)));

    return builder;
  }

  private static WebApplicationBuilder AddJsonSettings(this WebApplicationBuilder builder)
  {
    builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
              options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
              options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

    return builder;
  }

  private static WebApplicationBuilder AddAuth0Authentication(this WebApplicationBuilder builder)
  {
    var auth0Domain = builder.Configuration["Auth0:Domain"];
    var auth0Audience = builder.Configuration["Auth0:Audience"];

    builder.Services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
      options.Authority = auth0Domain;
      options.Audience = auth0Audience;

      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true
      };

      // 🚀 A SOLUÇÃO DO TIMEOUT/500 LOCAL:
      // Desabilita a exigência estrita de HTTPS se estiver rodando na sua máquina de desenvolvimento
      if (builder.Environment.IsDevelopment())
      {
        options.RequireHttpsMetadata = false;
      }
    });

    return builder;
  }
}
