using Mpx.Domain.Entities;

namespace Mpx.Domain.Interfaces.Entities
{
  public interface IUserRepository : IMorphusRepository<User>
  {
    Task<List<User>> GetList();
    Task<User?> GetByRefreshToken(string refreshToken);
    Task<User?> GetByLogin(string email);
    Task<User?> GetByEmail(string? email);
    Task<bool> ExistsEmail(string email, Guid? userId = null);
  }
}

