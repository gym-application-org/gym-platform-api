using System.Linq.Expressions;
using Application.Features.DietTemplates.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.DietTemplates;

public class DietTemplateManager : IDietTemplateService
{
    private readonly IDietTemplateRepository _dietTemplateRepository;
    private readonly DietTemplateBusinessRules _dietTemplateBusinessRules;

    public DietTemplateManager(IDietTemplateRepository dietTemplateRepository, DietTemplateBusinessRules dietTemplateBusinessRules)
    {
        _dietTemplateRepository = dietTemplateRepository;
        _dietTemplateBusinessRules = dietTemplateBusinessRules;
    }

    public async Task<DietTemplate?> GetAsync(
        Expression<Func<DietTemplate, bool>> predicate,
        Func<IQueryable<DietTemplate>, IIncludableQueryable<DietTemplate, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        DietTemplate? dietTemplate = await _dietTemplateRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return dietTemplate;
    }

    public async Task<IPaginate<DietTemplate>?> GetListAsync(
        Expression<Func<DietTemplate, bool>>? predicate = null,
        Func<IQueryable<DietTemplate>, IOrderedQueryable<DietTemplate>>? orderBy = null,
        Func<IQueryable<DietTemplate>, IIncludableQueryable<DietTemplate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<DietTemplate> dietTemplateList = await _dietTemplateRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return dietTemplateList;
    }

    public async Task<DietTemplate> AddAsync(DietTemplate dietTemplate)
    {
        DietTemplate addedDietTemplate = await _dietTemplateRepository.AddAsync(dietTemplate);

        return addedDietTemplate;
    }

    public async Task<DietTemplate> UpdateAsync(DietTemplate dietTemplate)
    {
        DietTemplate updatedDietTemplate = await _dietTemplateRepository.UpdateAsync(dietTemplate);

        return updatedDietTemplate;
    }

    public async Task<DietTemplate> DeleteAsync(DietTemplate dietTemplate, bool permanent = false)
    {
        DietTemplate deletedDietTemplate = await _dietTemplateRepository.DeleteAsync(dietTemplate);

        return deletedDietTemplate;
    }
}
