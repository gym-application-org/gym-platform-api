using System.Linq.Expressions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.DietTemplateDays;

public interface IDietTemplateDayService
{
    Task<DietTemplateDay?> GetAsync(
        Expression<Func<DietTemplateDay, bool>> predicate,
        Func<IQueryable<DietTemplateDay>, IIncludableQueryable<DietTemplateDay, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<DietTemplateDay>?> GetListAsync(
        Expression<Func<DietTemplateDay, bool>>? predicate = null,
        Func<IQueryable<DietTemplateDay>, IOrderedQueryable<DietTemplateDay>>? orderBy = null,
        Func<IQueryable<DietTemplateDay>, IIncludableQueryable<DietTemplateDay, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<DietTemplateDay> AddAsync(DietTemplateDay dietTemplateDay);
    Task<DietTemplateDay> UpdateAsync(DietTemplateDay dietTemplateDay);
    Task<DietTemplateDay> DeleteAsync(DietTemplateDay dietTemplateDay, bool permanent = false);
}
