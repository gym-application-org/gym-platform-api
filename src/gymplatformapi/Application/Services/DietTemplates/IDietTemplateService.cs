using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.DietTemplates;

public interface IDietTemplateService
{
    Task<DietTemplate?> GetAsync(
        Expression<Func<DietTemplate, bool>> predicate,
        Func<IQueryable<DietTemplate>, IIncludableQueryable<DietTemplate, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<DietTemplate>?> GetListAsync(
        Expression<Func<DietTemplate, bool>>? predicate = null,
        Func<IQueryable<DietTemplate>, IOrderedQueryable<DietTemplate>>? orderBy = null,
        Func<IQueryable<DietTemplate>, IIncludableQueryable<DietTemplate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<DietTemplate> AddAsync(DietTemplate dietTemplate);
    Task<DietTemplate> UpdateAsync(DietTemplate dietTemplate);
    Task<DietTemplate> DeleteAsync(DietTemplate dietTemplate, bool permanent = false);
}
