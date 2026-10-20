using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Mpx.Infrastructure.Interfaces;

namespace Mpx.Infrastructure.MultiTenant
{
  public class TenantProvider : ITenantProvider
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string HostTenantId = "HTENANT";

    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public string GetTenantId()
    {
      var user = _httpContextAccessor.HttpContext?.User;

      if (user == null || !user.Identity?.IsAuthenticated == true)
      {
        throw new UnauthorizedAccessException("Usuário não autenticado no Morphus.");
      }

      // 🛡️ Busca o ID da empresa vindo carimbado do Auth0
      var orgId = user.FindFirst("org_id")?.Value;

      if (!string.IsNullOrEmpty(orgId))
      {
        return orgId;
      }

      // Se for você, sem organização no token, mas com a role de admin global
      if (user.IsInRole("MorphusPlatformAdmin"))
      {
        return HostTenantId;
      }

      throw new UnauthorizedAccessException("Acesso negado: Organização não identificada.");
    }

    public bool IsHostAdmin()
    {
      var user = _httpContextAccessor.HttpContext?.User;
      var hasOrg = !string.IsNullOrEmpty(user?.FindFirst("org_id")?.Value);
      var isAdmin = user?.IsInRole("MorphusPlatformAdmin") ?? false;

      return !hasOrg && isAdmin;
    }
  }
}
