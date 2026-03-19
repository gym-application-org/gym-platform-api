using System.Linq.Expressions;
using Application.Features.Exercises.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.Exercises;

public class ExerciseManager : IExerciseService
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ExerciseBusinessRules _exerciseBusinessRules;

    public ExerciseManager(IExerciseRepository exerciseRepository, ExerciseBusinessRules exerciseBusinessRules)
    {
        _exerciseRepository = exerciseRepository;
        _exerciseBusinessRules = exerciseBusinessRules;
    }

    public async Task<Exercise?> GetAsync(
        Expression<Func<Exercise, bool>> predicate,
        Func<IQueryable<Exercise>, IIncludableQueryable<Exercise, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Exercise? exercise = await _exerciseRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            ignoreQueryFilters,
            enableTracking,
            cancellationToken
        );
        return exercise;
    }

    public async Task<IPaginate<Exercise>?> GetListAsync(
        Expression<Func<Exercise, bool>>? predicate = null,
        Func<IQueryable<Exercise>, IOrderedQueryable<Exercise>>? orderBy = null,
        Func<IQueryable<Exercise>, IIncludableQueryable<Exercise, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Exercise> exerciseList = await _exerciseRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            ignoreQueryFilters,
            enableTracking,
            cancellationToken
        );
        return exerciseList;
    }

    public async Task<Exercise> AddAsync(Exercise exercise)
    {
        Exercise addedExercise = await _exerciseRepository.AddAsync(exercise);

        return addedExercise;
    }

    public async Task<Exercise> UpdateAsync(Exercise exercise)
    {
        Exercise updatedExercise = await _exerciseRepository.UpdateAsync(exercise);

        return updatedExercise;
    }

    public async Task<Exercise> DeleteAsync(Exercise exercise, bool permanent = false)
    {
        Exercise deletedExercise = await _exerciseRepository.DeleteAsync(exercise);

        return deletedExercise;
    }
}
