using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.DietAssignments;

public interface IDietAssignmentService
{
    Task<DietAssignment?> GetAsync(
        Expression<Func<DietAssignment, bool>> predicate,
        Func<IQueryable<DietAssignment>, IIncludableQueryable<DietAssignment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<DietAssignment>?> GetListAsync(
        Expression<Func<DietAssignment, bool>>? predicate = null,
        Func<IQueryable<DietAssignment>, IOrderedQueryable<DietAssignment>>? orderBy = null,
        Func<IQueryable<DietAssignment>, IIncludableQueryable<DietAssignment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<DietAssignment> AddAsync(DietAssignment dietAssignment);
    Task<DietAssignment> UpdateAsync(DietAssignment dietAssignment);
    Task<DietAssignment> DeleteAsync(DietAssignment dietAssignment, bool permanent = false);
}
