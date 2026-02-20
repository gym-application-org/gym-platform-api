using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class WorkoutTemplateRepository : EfRepositoryBase<WorkoutTemplate, int, BaseDbContext>, IWorkoutTemplateRepository
{
    public WorkoutTemplateRepository(BaseDbContext context)
        : base(context) { }
}
