using Microsoft.EntityFrameworkCore;
using Morphus.Infrastructure.Data;
using mpx.Domain.Interfaces;
using Mpx.Domain.Entities;
using Mpx.Domain.Interfaces;
using Mpx.Infrastructure.Interfaces;

namespace Mpx.Infrastructure.Data;

public class MorphusDbContext(DbContextOptions<MorphusDbContext> options, ITenantProvider tenantProvider) : DbContext(options), IUnitOfWork
{

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.ApplyConfigurationsFromAssembly(typeof(MorphusDbContext).Assembly);

    foreach (var entityType in builder.Model.GetEntityTypes())
    {
      if (typeof(IMorphusTenant).IsAssignableFrom(entityType.ClrType))
      {
        builder.Entity(entityType.ClrType)
            .Property(nameof(IMorphusTenant.TenantId))
            .HasColumnName("tenant_id")
            .IsRequired();

        // Usa o tenantProvider do construtor primário direto aqui!
        builder.Entity(entityType.ClrType)
            .HasQueryFilter(ConvertFilterExpression(entityType.ClrType));
      }
    }
  }

  private System.Linq.Expressions.LambdaExpression ConvertFilterExpression(Type type)
  {
    var parameter = System.Linq.Expressions.Expression.Parameter(type, "e");
    var property = System.Linq.Expressions.Expression.Property(parameter, nameof(IMorphusTenant.TenantId));

    var providerExpression = System.Linq.Expressions.Expression.Call(
        System.Linq.Expressions.Expression.Constant(tenantProvider), // Sem o '_' do campo privado
        typeof(ITenantProvider).GetMethod(nameof(ITenantProvider.GetTenantId))!
    );

    var isAdminExpression = System.Linq.Expressions.Expression.Call(
        System.Linq.Expressions.Expression.Constant(tenantProvider),
        typeof(ITenantProvider).GetMethod(nameof(ITenantProvider.IsHostAdmin))!
    );

    var condition = System.Linq.Expressions.Expression.OrElse(
        System.Linq.Expressions.Expression.Equal(property, providerExpression),
        isAdminExpression
    );

    return System.Linq.Expressions.Expression.Lambda(condition, parameter);
  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    // Usa o tenantProvider do construtor primário direto aqui também!
    if (!tenantProvider.IsHostAdmin())
    {
      var currentTenantId = tenantProvider.GetTenantId();

      foreach (var entry in ChangeTracker.Entries<IMorphusTenant>())
      {
        if (entry.State == EntityState.Added)
        {
          entry.Entity.TenantId = currentTenantId;
        }
      }
    }

    return base.SaveChangesAsync(cancellationToken);
  }

  public DbSet<Corporation> Corporations { get; set; }
  public DbSet<Menu> Menus { get; set; }
  public DbSet<Module> Modules { get; set; }
  public DbSet<SubModule> SubModules { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<UserStatus> UserStatus { get; set; }

  public DbSet<Email> Emails { get; set; }
  public DbSet<EmailTo> EmailsTo { get; set; }
  public DbSet<EmailFrom> EmailsFrom { get; set; }
  public DbSet<EmailCc> EmailsCc { get; set; }
  public DbSet<EmailBcc> EmailsBcc { get; set; }

  // Implementação do IUnitOfWork: Cria a transação do EF e encapsula na sua classe concreta
  public async Task<IMorphusTransaction> BeginTransactionAsync()
  {
    var efTransaction = await Database.BeginTransactionAsync();
    return new MorphusTransaction(efTransaction);
  }

  // Implementação do IUnitOfWork: Salva as alterações pendentes no banco
  public async Task<int> SaveChangesAsync()
  {
    return await base.SaveChangesAsync();
  }
}
