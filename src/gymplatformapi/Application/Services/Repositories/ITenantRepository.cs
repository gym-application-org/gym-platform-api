using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ITenantRepository : IAsyncRepository<Tenant, Guid>, IRepository<Tenant, Guid> { }
