using Mpx.Domain.Entities;

namespace Mpx.Domain.Interfaces.Entities
{
  public interface IUserStatusRepository : IMorphusRepository<UserStatus>
  {
    Task<List<UserStatus>> GetList();
  }

}

