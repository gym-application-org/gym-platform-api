using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class StaffRepository : EfRepositoryBase<Staff, Guid, BaseDbContext>, IStaffRepository
{
    public StaffRepository(BaseDbContext context)
        : base(context) { }
}
