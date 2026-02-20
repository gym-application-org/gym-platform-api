using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DietAssignmentRepository : EfRepositoryBase<DietAssignment, int, BaseDbContext>, IDietAssignmentRepository
{
    public DietAssignmentRepository(BaseDbContext context)
        : base(context) { }
}
