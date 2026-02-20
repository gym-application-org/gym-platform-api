using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class WorkoutTemplateDayRepository : EfRepositoryBase<WorkoutTemplateDay, int, BaseDbContext>, IWorkoutTemplateDayRepository
{
    public WorkoutTemplateDayRepository(BaseDbContext context)
        : base(context) { }
}
