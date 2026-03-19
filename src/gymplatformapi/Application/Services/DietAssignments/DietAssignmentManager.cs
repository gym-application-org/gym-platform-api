using System.Linq.Expressions;
using Application.Features.DietAssignments.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.DietAssignments;

public class DietAssignmentManager : IDietAssignmentService
{
    private readonly IDietAssignmentRepository _dietAssignmentRepository;
    private readonly DietAssignmentBusinessRules _dietAssignmentBusinessRules;

    public DietAssignmentManager(
        IDietAssignmentRepository dietAssignmentRepository,
        DietAssignmentBusinessRules dietAssignmentBusinessRules
    )
    {
        _dietAssignmentRepository = dietAssignmentRepository;
        _dietAssignmentBusinessRules = dietAssignmentBusinessRules;
    }

    public async Task<DietAssignment?> GetAsync(
        Expression<Func<DietAssignment, bool>> predicate,
        Func<IQueryable<DietAssignment>, IIncludableQueryable<DietAssignment, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        DietAssignment? dietAssignment = await _dietAssignmentRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            ignoreQueryFilters,
            enableTracking,
            cancellationToken
        );
        return dietAssignment;
    }

    public async Task<IPaginate<DietAssignment>?> GetListAsync(
        Expression<Func<DietAssignment, bool>>? predicate = null,
        Func<IQueryable<DietAssignment>, IOrderedQueryable<DietAssignment>>? orderBy = null,
        Func<IQueryable<DietAssignment>, IIncludableQueryable<DietAssignment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<DietAssignment> dietAssignmentList = await _dietAssignmentRepository.GetListAsync(
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
        return dietAssignmentList;
    }

    public async Task<DietAssignment> AddAsync(DietAssignment dietAssignment)
    {
        DietAssignment addedDietAssignment = await _dietAssignmentRepository.AddAsync(dietAssignment);

        return addedDietAssignment;
    }

    public async Task<DietAssignment> UpdateAsync(DietAssignment dietAssignment)
    {
        DietAssignment updatedDietAssignment = await _dietAssignmentRepository.UpdateAsync(dietAssignment);

        return updatedDietAssignment;
    }

    public async Task<DietAssignment> DeleteAsync(DietAssignment dietAssignment, bool permanent = false)
    {
        DietAssignment deletedDietAssignment = await _dietAssignmentRepository.DeleteAsync(dietAssignment);

        return deletedDietAssignment;
    }
}
