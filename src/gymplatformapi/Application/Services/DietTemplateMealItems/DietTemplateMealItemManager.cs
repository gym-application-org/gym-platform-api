using System.Linq.Expressions;
using Application.Features.DietTemplateMealItems.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.DietTemplateMealItems;

public class DietTemplateMealItemManager : IDietTemplateMealItemService
{
    private readonly IDietTemplateMealItemRepository _dietTemplateMealItemRepository;
    private readonly DietTemplateMealItemBusinessRules _dietTemplateMealItemBusinessRules;

    public DietTemplateMealItemManager(
        IDietTemplateMealItemRepository dietTemplateMealItemRepository,
        DietTemplateMealItemBusinessRules dietTemplateMealItemBusinessRules
    )
    {
        _dietTemplateMealItemRepository = dietTemplateMealItemRepository;
        _dietTemplateMealItemBusinessRules = dietTemplateMealItemBusinessRules;
    }

    public async Task<DietTemplateMealItem?> GetAsync(
        Expression<Func<DietTemplateMealItem, bool>> predicate,
        Func<IQueryable<DietTemplateMealItem>, IIncludableQueryable<DietTemplateMealItem, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        DietTemplateMealItem? dietTemplateMealItem = await _dietTemplateMealItemRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            ignoreQueryFilters,
            enableTracking,
            cancellationToken
        );
        return dietTemplateMealItem;
    }

    public async Task<IPaginate<DietTemplateMealItem>?> GetListAsync(
        Expression<Func<DietTemplateMealItem, bool>>? predicate = null,
        Func<IQueryable<DietTemplateMealItem>, IOrderedQueryable<DietTemplateMealItem>>? orderBy = null,
        Func<IQueryable<DietTemplateMealItem>, IIncludableQueryable<DietTemplateMealItem, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<DietTemplateMealItem> dietTemplateMealItemList = await _dietTemplateMealItemRepository.GetListAsync(
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
        return dietTemplateMealItemList;
    }

    public async Task<DietTemplateMealItem> AddAsync(DietTemplateMealItem dietTemplateMealItem)
    {
        DietTemplateMealItem addedDietTemplateMealItem = await _dietTemplateMealItemRepository.AddAsync(dietTemplateMealItem);

        return addedDietTemplateMealItem;
    }

    public async Task<DietTemplateMealItem> UpdateAsync(DietTemplateMealItem dietTemplateMealItem)
    {
        DietTemplateMealItem updatedDietTemplateMealItem = await _dietTemplateMealItemRepository.UpdateAsync(dietTemplateMealItem);

        return updatedDietTemplateMealItem;
    }

    public async Task<DietTemplateMealItem> DeleteAsync(DietTemplateMealItem dietTemplateMealItem, bool permanent = false)
    {
        DietTemplateMealItem deletedDietTemplateMealItem = await _dietTemplateMealItemRepository.DeleteAsync(dietTemplateMealItem);

        return deletedDietTemplateMealItem;
    }
}
