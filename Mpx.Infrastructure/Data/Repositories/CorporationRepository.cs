using Mpx.Domain.Entities;
using Mpx.Domain.Interfaces;

namespace Mpx.Infrastructure.Data.Repositories;

public class CorporationRepository(MorphusDbContext Context) : MorphusRepository<Corporation>(Context), ICorporationRepository
{
  //protected readonly MorphusDbContext _context = Context;


}
