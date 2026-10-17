using Microsoft.EntityFrameworkCore;
using Morphus.Infrastructure.Data;
using mpx.Domain.Interfaces;
using Mpx.Domain.Entities;
using Mpx.Domain.Interfaces;

namespace Mpx.Infrastructure.Data;

public class MorphusDbContext(DbContextOptions<MorphusDbContext> options) : DbContext(options), IUnitOfWork
{
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.ApplyConfigurationsFromAssembly(typeof(MorphusDbContext).Assembly);
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
