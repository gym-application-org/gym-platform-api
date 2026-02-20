using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.WorkoutTemplateDayExercises;

public interface IWorkoutTemplateDayExerciseService
{
    Task<WorkoutTemplateDayExercise?> GetAsync(
        Expression<Func<WorkoutTemplateDayExercise, bool>> predicate,
        Func<IQueryable<WorkoutTemplateDayExercise>, IIncludableQueryable<WorkoutTemplateDayExercise, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<WorkoutTemplateDayExercise>?> GetListAsync(
        Expression<Func<WorkoutTemplateDayExercise, bool>>? predicate = null,
        Func<IQueryable<WorkoutTemplateDayExercise>, IOrderedQueryable<WorkoutTemplateDayExercise>>? orderBy = null,
        Func<IQueryable<WorkoutTemplateDayExercise>, IIncludableQueryable<WorkoutTemplateDayExercise, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<WorkoutTemplateDayExercise> AddAsync(WorkoutTemplateDayExercise workoutTemplateDayExercise);
    Task<WorkoutTemplateDayExercise> UpdateAsync(WorkoutTemplateDayExercise workoutTemplateDayExercise);
    Task<WorkoutTemplateDayExercise> DeleteAsync(WorkoutTemplateDayExercise workoutTemplateDayExercise, bool permanent = false);
}
