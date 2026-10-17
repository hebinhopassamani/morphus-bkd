using Mpx.Domain.Entities;

namespace Mpx.Domain.Interfaces
{
  public interface IModuleRepository : IMorphusRepository<Module>
  {
    Task<List<SubModule>> GetSubModules(Guid id);
    Task<List<Module>> GetModulesByKey(string key);
    Task<List<Module>> GetList();
  }
}

