namespace Mpx.Infrastructure.Interfaces
{
  public interface ITenantProvider
  {
    string GetTenantId();
    bool IsHostAdmin();
  }
}
