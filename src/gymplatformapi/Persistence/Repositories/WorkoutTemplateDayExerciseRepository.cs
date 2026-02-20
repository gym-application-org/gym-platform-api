using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class WorkoutTemplateDayExerciseRepository
    : EfRepositoryBase<WorkoutTemplateDayExercise, int, BaseDbContext>,
        IWorkoutTemplateDayExerciseRepository
{
    public WorkoutTemplateDayExerciseRepository(BaseDbContext context)
        : base(context) { }
}
