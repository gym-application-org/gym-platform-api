using System.Linq.Expressions;
using Application.Features.DietTemplateMeals.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.DietTemplateMeals;

public class DietTemplateMealManager : IDietTemplateMealService
{
    private readonly IDietTemplateMealRepository _dietTemplateMealRepository;
    private readonly DietTemplateMealBusinessRules _dietTemplateMealBusinessRules;

    public DietTemplateMealManager(
        IDietTemplateMealRepository dietTemplateMealRepository,
        DietTemplateMealBusinessRules dietTemplateMealBusinessRules
    )
    {
        _dietTemplateMealRepository = dietTemplateMealRepository;
        _dietTemplateMealBusinessRules = dietTemplateMealBusinessRules;
    }

    public async Task<DietTemplateMeal?> GetAsync(
        Expression<Func<DietTemplateMeal, bool>> predicate,
        Func<IQueryable<DietTemplateMeal>, IIncludableQueryable<DietTemplateMeal, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        DietTemplateMeal? dietTemplateMeal = await _dietTemplateMealRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return dietTemplateMeal;
    }

    public async Task<IPaginate<DietTemplateMeal>?> GetListAsync(
        Expression<Func<DietTemplateMeal, bool>>? predicate = null,
        Func<IQueryable<DietTemplateMeal>, IOrderedQueryable<DietTemplateMeal>>? orderBy = null,
        Func<IQueryable<DietTemplateMeal>, IIncludableQueryable<DietTemplateMeal, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<DietTemplateMeal> dietTemplateMealList = await _dietTemplateMealRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return dietTemplateMealList;
    }

    public async Task<DietTemplateMeal> AddAsync(DietTemplateMeal dietTemplateMeal)
    {
        DietTemplateMeal addedDietTemplateMeal = await _dietTemplateMealRepository.AddAsync(dietTemplateMeal);

        return addedDietTemplateMeal;
    }

    public async Task<DietTemplateMeal> UpdateAsync(DietTemplateMeal dietTemplateMeal)
    {
        DietTemplateMeal updatedDietTemplateMeal = await _dietTemplateMealRepository.UpdateAsync(dietTemplateMeal);

        return updatedDietTemplateMeal;
    }

    public async Task<DietTemplateMeal> DeleteAsync(DietTemplateMeal dietTemplateMeal, bool permanent = false)
    {
        DietTemplateMeal deletedDietTemplateMeal = await _dietTemplateMealRepository.DeleteAsync(dietTemplateMeal);

        return deletedDietTemplateMeal;
    }
}
