using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TenantRepository : EfRepositoryBase<Tenant, Guid, BaseDbContext>, ITenantRepository
{
    public TenantRepository(BaseDbContext context)
        : base(context) { }
}
