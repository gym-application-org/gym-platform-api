using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class GateRepository : EfRepositoryBase<Gate, int, BaseDbContext>, IGateRepository
{
    public GateRepository(BaseDbContext context)
        : base(context) { }
}
