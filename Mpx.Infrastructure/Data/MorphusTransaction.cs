using Microsoft.EntityFrameworkCore.Storage;
using Mpx.Domain.Interfaces;

namespace Morphus.Infrastructure.Data;

public class MorphusTransaction(IDbContextTransaction efTransaction) : IMorphusTransaction
{
  private readonly IDbContextTransaction _efTransaction = efTransaction ?? throw new ArgumentNullException(nameof(efTransaction));

  // Implementa exatamente o método "Commit()" da sua interface
  public async Task<int> Commit()
  {
    await _efTransaction.CommitAsync(); // Chama o método nativo assíncrono do EF
    return 1;
  }

  // Implementa exatamente o método "Rollback()" da sua interface
  public async Task Rollback()
  {
    await _efTransaction.RollbackAsync(); // Chama o método nativo assíncrono do EF
  }

  public void Dispose()
  {
    _efTransaction.Dispose();
    GC.SuppressFinalize(this);
  }
}
