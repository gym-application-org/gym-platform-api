using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class WorkoutAssignmentRepository : EfRepositoryBase<WorkoutAssignment, int, BaseDbContext>, IWorkoutAssignmentRepository
{
    public WorkoutAssignmentRepository(BaseDbContext context)
        : base(context) { }
}
