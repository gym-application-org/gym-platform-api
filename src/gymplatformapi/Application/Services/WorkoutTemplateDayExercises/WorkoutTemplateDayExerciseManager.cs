using System.Linq.Expressions;
using Application.Features.WorkoutTemplateDayExercises.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.WorkoutTemplateDayExercises;

public class WorkoutTemplateDayExerciseManager : IWorkoutTemplateDayExerciseService
{
    private readonly IWorkoutTemplateDayExerciseRepository _workoutTemplateDayExerciseRepository;
    private readonly WorkoutTemplateDayExerciseBusinessRules _workoutTemplateDayExerciseBusinessRules;

    public WorkoutTemplateDayExerciseManager(
        IWorkoutTemplateDayExerciseRepository workoutTemplateDayExerciseRepository,
        WorkoutTemplateDayExerciseBusinessRules workoutTemplateDayExerciseBusinessRules
    )
    {
        _workoutTemplateDayExerciseRepository = workoutTemplateDayExerciseRepository;
        _workoutTemplateDayExerciseBusinessRules = workoutTemplateDayExerciseBusinessRules;
    }

    public async Task<WorkoutTemplateDayExercise?> GetAsync(
        Expression<Func<WorkoutTemplateDayExercise, bool>> predicate,
        Func<IQueryable<WorkoutTemplateDayExercise>, IIncludableQueryable<WorkoutTemplateDayExercise, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        WorkoutTemplateDayExercise? workoutTemplateDayExercise = await _workoutTemplateDayExerciseRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return workoutTemplateDayExercise;
    }

    public async Task<IPaginate<WorkoutTemplateDayExercise>?> GetListAsync(
        Expression<Func<WorkoutTemplateDayExercise, bool>>? predicate = null,
        Func<IQueryable<WorkoutTemplateDayExercise>, IOrderedQueryable<WorkoutTemplateDayExercise>>? orderBy = null,
        Func<IQueryable<WorkoutTemplateDayExercise>, IIncludableQueryable<WorkoutTemplateDayExercise, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<WorkoutTemplateDayExercise> workoutTemplateDayExerciseList = await _workoutTemplateDayExerciseRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return workoutTemplateDayExerciseList;
    }

    public async Task<WorkoutTemplateDayExercise> AddAsync(WorkoutTemplateDayExercise workoutTemplateDayExercise)
    {
        WorkoutTemplateDayExercise addedWorkoutTemplateDayExercise = await _workoutTemplateDayExerciseRepository.AddAsync(
            workoutTemplateDayExercise
        );

        return addedWorkoutTemplateDayExercise;
    }

    public async Task<WorkoutTemplateDayExercise> UpdateAsync(WorkoutTemplateDayExercise workoutTemplateDayExercise)
    {
        WorkoutTemplateDayExercise updatedWorkoutTemplateDayExercise = await _workoutTemplateDayExerciseRepository.UpdateAsync(
            workoutTemplateDayExercise
        );

        return updatedWorkoutTemplateDayExercise;
    }

    public async Task<WorkoutTemplateDayExercise> DeleteAsync(WorkoutTemplateDayExercise workoutTemplateDayExercise, bool permanent = false)
    {
        WorkoutTemplateDayExercise deletedWorkoutTemplateDayExercise = await _workoutTemplateDayExerciseRepository.DeleteAsync(
            workoutTemplateDayExercise
        );

        return deletedWorkoutTemplateDayExercise;
    }
}
