using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.Gates;

public interface IGateService
{
    Task<Gate?> GetAsync(
        Expression<Func<Gate, bool>> predicate,
        Func<IQueryable<Gate>, IIncludableQueryable<Gate, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Gate>?> GetListAsync(
        Expression<Func<Gate, bool>>? predicate = null,
        Func<IQueryable<Gate>, IOrderedQueryable<Gate>>? orderBy = null,
        Func<IQueryable<Gate>, IIncludableQueryable<Gate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Gate> AddAsync(Gate gate);
    Task<Gate> UpdateAsync(Gate gate);
    Task<Gate> DeleteAsync(Gate gate, bool permanent = false);
}
