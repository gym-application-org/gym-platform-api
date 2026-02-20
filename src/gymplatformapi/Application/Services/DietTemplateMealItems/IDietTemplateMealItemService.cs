using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.DietTemplateMealItems;

public interface IDietTemplateMealItemService
{
    Task<DietTemplateMealItem?> GetAsync(
        Expression<Func<DietTemplateMealItem, bool>> predicate,
        Func<IQueryable<DietTemplateMealItem>, IIncludableQueryable<DietTemplateMealItem, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<DietTemplateMealItem>?> GetListAsync(
        Expression<Func<DietTemplateMealItem, bool>>? predicate = null,
        Func<IQueryable<DietTemplateMealItem>, IOrderedQueryable<DietTemplateMealItem>>? orderBy = null,
        Func<IQueryable<DietTemplateMealItem>, IIncludableQueryable<DietTemplateMealItem, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<DietTemplateMealItem> AddAsync(DietTemplateMealItem dietTemplateMealItem);
    Task<DietTemplateMealItem> UpdateAsync(DietTemplateMealItem dietTemplateMealItem);
    Task<DietTemplateMealItem> DeleteAsync(DietTemplateMealItem dietTemplateMealItem, bool permanent = false);
}
