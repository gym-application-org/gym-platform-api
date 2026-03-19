using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.WorkoutTemplates;

public interface IWorkoutTemplateService
{
    Task<WorkoutTemplate?> GetAsync(
        Expression<Func<WorkoutTemplate, bool>> predicate,
        Func<IQueryable<WorkoutTemplate>, IIncludableQueryable<WorkoutTemplate, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<WorkoutTemplate>?> GetListAsync(
        Expression<Func<WorkoutTemplate, bool>>? predicate = null,
        Func<IQueryable<WorkoutTemplate>, IOrderedQueryable<WorkoutTemplate>>? orderBy = null,
        Func<IQueryable<WorkoutTemplate>, IIncludableQueryable<WorkoutTemplate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<WorkoutTemplate> AddAsync(WorkoutTemplate workoutTemplate);
    Task<WorkoutTemplate> UpdateAsync(WorkoutTemplate workoutTemplate);
    Task<WorkoutTemplate> DeleteAsync(WorkoutTemplate workoutTemplate, bool permanent = false);
}
