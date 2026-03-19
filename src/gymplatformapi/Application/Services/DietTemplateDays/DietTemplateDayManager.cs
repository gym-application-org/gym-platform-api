using System.Linq.Expressions;
using Application.Features.DietTemplateDays.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.DietTemplateDays;

public class DietTemplateDayManager : IDietTemplateDayService
{
    private readonly IDietTemplateDayRepository _dietTemplateDayRepository;
    private readonly DietTemplateDayBusinessRules _dietTemplateDayBusinessRules;

    public DietTemplateDayManager(
        IDietTemplateDayRepository dietTemplateDayRepository,
        DietTemplateDayBusinessRules dietTemplateDayBusinessRules
    )
    {
        _dietTemplateDayRepository = dietTemplateDayRepository;
        _dietTemplateDayBusinessRules = dietTemplateDayBusinessRules;
    }

    public async Task<DietTemplateDay?> GetAsync(
        Expression<Func<DietTemplateDay, bool>> predicate,
        Func<IQueryable<DietTemplateDay>, IIncludableQueryable<DietTemplateDay, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        DietTemplateDay? dietTemplateDay = await _dietTemplateDayRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            ignoreQueryFilters,
            enableTracking,
            cancellationToken
        );
        return dietTemplateDay;
    }

    public async Task<IPaginate<DietTemplateDay>?> GetListAsync(
        Expression<Func<DietTemplateDay, bool>>? predicate = null,
        Func<IQueryable<DietTemplateDay>, IOrderedQueryable<DietTemplateDay>>? orderBy = null,
        Func<IQueryable<DietTemplateDay>, IIncludableQueryable<DietTemplateDay, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<DietTemplateDay> dietTemplateDayList = await _dietTemplateDayRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            ignoreQueryFilters,
            enableTracking,
            cancellationToken
        );
        return dietTemplateDayList;
    }

    public async Task<DietTemplateDay> AddAsync(DietTemplateDay dietTemplateDay)
    {
        DietTemplateDay addedDietTemplateDay = await _dietTemplateDayRepository.AddAsync(dietTemplateDay);

        return addedDietTemplateDay;
    }

    public async Task<DietTemplateDay> UpdateAsync(DietTemplateDay dietTemplateDay)
    {
        DietTemplateDay updatedDietTemplateDay = await _dietTemplateDayRepository.UpdateAsync(dietTemplateDay);

        return updatedDietTemplateDay;
    }

    public async Task<DietTemplateDay> DeleteAsync(DietTemplateDay dietTemplateDay, bool permanent = false)
    {
        DietTemplateDay deletedDietTemplateDay = await _dietTemplateDayRepository.DeleteAsync(dietTemplateDay);

        return deletedDietTemplateDay;
    }
}
