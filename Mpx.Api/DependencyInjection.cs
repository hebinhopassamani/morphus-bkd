using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using mpx.Domain.Interfaces;
using Mpx.Infrastructure.Data;

namespace Mpx.Api;

public static class ApiDI
{
  public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
  {

    services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<MorphusDbContext>());
    return services;
  }

  public static IServiceCollection AddJsonSettings(this IServiceCollection services)
  {
    services.AddControllers()
            .AddJsonOptions(options =>
            {
              options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
              options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

    return services;
  }

  public static IServiceCollection AddAuth0Authentication(this IServiceCollection services, IConfiguration configuration)
  {
    var auth0Domain = configuration["Auth0:Domain"];
    var auth0Audience = configuration["Auth0:Audience"];

    services.AddAuthentication(options =>
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
    });

    return services;
  }
}
