using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.WorkoutAssignments;

public interface IWorkoutAssignmentService
{
    Task<WorkoutAssignment?> GetAsync(
        Expression<Func<WorkoutAssignment, bool>> predicate,
        Func<IQueryable<WorkoutAssignment>, IIncludableQueryable<WorkoutAssignment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<WorkoutAssignment>?> GetListAsync(
        Expression<Func<WorkoutAssignment, bool>>? predicate = null,
        Func<IQueryable<WorkoutAssignment>, IOrderedQueryable<WorkoutAssignment>>? orderBy = null,
        Func<IQueryable<WorkoutAssignment>, IIncludableQueryable<WorkoutAssignment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<WorkoutAssignment> AddAsync(WorkoutAssignment workoutAssignment);
    Task<WorkoutAssignment> UpdateAsync(WorkoutAssignment workoutAssignment);
    Task<WorkoutAssignment> DeleteAsync(WorkoutAssignment workoutAssignment, bool permanent = false);
}
