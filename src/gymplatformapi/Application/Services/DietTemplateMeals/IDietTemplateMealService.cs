using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.DietTemplateMeals;

public interface IDietTemplateMealService
{
    Task<DietTemplateMeal?> GetAsync(
        Expression<Func<DietTemplateMeal, bool>> predicate,
        Func<IQueryable<DietTemplateMeal>, IIncludableQueryable<DietTemplateMeal, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<DietTemplateMeal>?> GetListAsync(
        Expression<Func<DietTemplateMeal, bool>>? predicate = null,
        Func<IQueryable<DietTemplateMeal>, IOrderedQueryable<DietTemplateMeal>>? orderBy = null,
        Func<IQueryable<DietTemplateMeal>, IIncludableQueryable<DietTemplateMeal, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<DietTemplateMeal> AddAsync(DietTemplateMeal dietTemplateMeal);
    Task<DietTemplateMeal> UpdateAsync(DietTemplateMeal dietTemplateMeal);
    Task<DietTemplateMeal> DeleteAsync(DietTemplateMeal dietTemplateMeal, bool permanent = false);
}
