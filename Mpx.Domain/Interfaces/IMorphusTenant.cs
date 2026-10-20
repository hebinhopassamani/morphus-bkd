namespace Mpx.Domain.Interfaces;

/// <summary>
/// Interface de marcação para o isolamento Multi-Tenant do Morphus.
/// Toda entidade que herdar esta interface ganhará a coluna 'tenant_id' automaticamente.
/// </summary>
public interface IMorphusTenant
{
  public string TenantId { get; set; }
}
