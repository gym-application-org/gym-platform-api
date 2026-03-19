using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.Staffs;

public interface IStaffService
{
    Task<Staff?> GetAsync(
        Expression<Func<Staff, bool>> predicate,
        Func<IQueryable<Staff>, IIncludableQueryable<Staff, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Staff>?> GetListAsync(
        Expression<Func<Staff, bool>>? predicate = null,
        Func<IQueryable<Staff>, IOrderedQueryable<Staff>>? orderBy = null,
        Func<IQueryable<Staff>, IIncludableQueryable<Staff, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Staff> AddAsync(Staff staff);
    Task<Staff> UpdateAsync(Staff staff);
    Task<Staff> DeleteAsync(Staff staff, bool permanent = false);
}
