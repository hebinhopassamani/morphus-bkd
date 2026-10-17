using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mpx.Domain.Interfaces;
using Mpx.Infrastructure.Data;
using Mpx.Infrastructure.Data.Repositories;
using Renci.SshNet;
using Npgsql;

namespace Mpx.Infrastructure;

public static class InfrastructureDI
{
  private static ForwardedPortLocal? _sshPort;
  private static SshClient? _sshClient;

  public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
  {
    services.AddScoped<ICorporationRepository, CorporationRepository>();
    services.AddScoped(typeof(IMorphusRepository<>), typeof(MorphusRepository<>));
    return services;
  }

  public static IServiceCollection AddMorhusDbContext(this IServiceCollection services, IConfiguration configuration)
  {
    var sshHost = configuration["SshTunnel:Host"]
    ?? throw new InvalidOperationException("A configuração 'SshTunnel:Host' não foi encontrada no appsettings.json.");

    var sshPort = int.Parse(configuration["SshTunnel:Port"] ?? "22");

    var sshUser = configuration["SshTunnel:Username"]
        ?? throw new InvalidOperationException("A configuração 'SshTunnel:Username' não foi encontrada no appsettings.json.");

    var sshPass = configuration["SshTunnel:Password"]
        ?? throw new InvalidOperationException("A configuração 'SshTunnel:Password' não foi encontrada no appsettings.json.");

    if (_sshClient == null)
    {
      _sshClient = new SshClient(sshHost, sshPort, sshUser, sshPass);
      _sshClient.Connect();

      // Escuta a porta local 5433 e redireciona para o localhost (127.0.0.1) na porta 5432 dentro da sua VPS
      _sshPort = new ForwardedPortLocal("127.0.0.1", 5433, "127.0.0.1", 5432);
      _sshClient.AddForwardedPort(_sshPort);
      _sshPort.Start();
    }

    var connectionString = configuration.GetConnectionString("DefaultConnection");

    services.AddDbContext<MorphusDbContext>(options => options
             .UseLazyLoadingProxies()
             .UseNpgsql(connectionString));
    // var connectionString = configuration.GetConnectionString("ConnectionString");

    // // var connectionBuilder = new NpgsqlConnectionStringBuilder(connectionString)
    // // {
    // //   Password = configuration["POSTGRES_DB_PASSWORD"] ?? throw new InvalidOperationException("Nenhuma senha de banco de dados configurada"),
    // // };

    // // var withPassword = connectionBuilder.ConnectionString;

    // services.AddDbContext<MorphusDbContext>(options => options
    //          .UseLazyLoadingProxies()
    //          .UseNpgsql(connectionString));

    return services;
  }
}
