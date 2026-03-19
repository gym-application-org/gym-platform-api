using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.WorkoutTemplateDays;

public interface IWorkoutTemplateDayService
{
    Task<WorkoutTemplateDay?> GetAsync(
        Expression<Func<WorkoutTemplateDay, bool>> predicate,
        Func<IQueryable<WorkoutTemplateDay>, IIncludableQueryable<WorkoutTemplateDay, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<WorkoutTemplateDay>?> GetListAsync(
        Expression<Func<WorkoutTemplateDay, bool>>? predicate = null,
        Func<IQueryable<WorkoutTemplateDay>, IOrderedQueryable<WorkoutTemplateDay>>? orderBy = null,
        Func<IQueryable<WorkoutTemplateDay>, IIncludableQueryable<WorkoutTemplateDay, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<WorkoutTemplateDay> AddAsync(WorkoutTemplateDay workoutTemplateDay);
    Task<WorkoutTemplateDay> UpdateAsync(WorkoutTemplateDay workoutTemplateDay);
    Task<WorkoutTemplateDay> DeleteAsync(WorkoutTemplateDay workoutTemplateDay, bool permanent = false);
}
