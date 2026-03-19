using System.Linq.Expressions;
using Application.Features.Gates.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services.Gates;

public class GateManager : IGateService
{
    private readonly IGateRepository _gateRepository;
    private readonly GateBusinessRules _gateBusinessRules;

    public GateManager(IGateRepository gateRepository, GateBusinessRules gateBusinessRules)
    {
        _gateRepository = gateRepository;
        _gateBusinessRules = gateBusinessRules;
    }

    public async Task<Gate?> GetAsync(
        Expression<Func<Gate, bool>> predicate,
        Func<IQueryable<Gate>, IIncludableQueryable<Gate, object>>? include = null,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Gate? gate = await _gateRepository.GetAsync(predicate, include, withDeleted, ignoreQueryFilters, enableTracking, cancellationToken);
        return gate;
    }

    public async Task<IPaginate<Gate>?> GetListAsync(
        Expression<Func<Gate, bool>>? predicate = null,
        Func<IQueryable<Gate>, IOrderedQueryable<Gate>>? orderBy = null,
        Func<IQueryable<Gate>, IIncludableQueryable<Gate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool ignoreQueryFilters = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Gate> gateList = await _gateRepository.GetListAsync(
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
        return gateList;
    }

    public async Task<Gate> AddAsync(Gate gate)
    {
        Gate addedGate = await _gateRepository.AddAsync(gate);

        return addedGate;
    }

    public async Task<Gate> UpdateAsync(Gate gate)
    {
        Gate updatedGate = await _gateRepository.UpdateAsync(gate);

        return updatedGate;
    }

    public async Task<Gate> DeleteAsync(Gate gate, bool permanent = false)
    {
        Gate deletedGate = await _gateRepository.DeleteAsync(gate);

        return deletedGate;
    }
}
