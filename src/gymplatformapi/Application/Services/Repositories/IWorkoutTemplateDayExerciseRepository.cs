using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IWorkoutTemplateDayExerciseRepository
    : IAsyncRepository<WorkoutTemplateDayExercise, int>,
        IRepository<WorkoutTemplateDayExercise, int> { }
