using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.Exercises;

public interface IExerciseService
{
    Task<Exercise?> GetAsync(
        Expression<Func<Exercise, bool>> predicate,
        Func<IQueryable<Exercise>, IIncludableQueryable<Exercise, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Exercise>?> GetListAsync(
        Expression<Func<Exercise, bool>>? predicate = null,
        Func<IQueryable<Exercise>, IOrderedQueryable<Exercise>>? orderBy = null,
        Func<IQueryable<Exercise>, IIncludableQueryable<Exercise, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Exercise> AddAsync(Exercise exercise);
    Task<Exercise> UpdateAsync(Exercise exercise);
    Task<Exercise> DeleteAsync(Exercise exercise, bool permanent = false);
}
