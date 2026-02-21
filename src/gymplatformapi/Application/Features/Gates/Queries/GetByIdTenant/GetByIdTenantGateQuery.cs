using Application.Features.Gates.Constants;
using Application.Features.Gates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Gates.Constants.GatesOperationClaims;

namespace Application.Features.Gates.Queries.GetByIdTenant;

public class GetByIdTenantGateQuery : IRequest<GetByIdTenantGateResponse>, ISecuredRequest, ITenantRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class GetByIdTenantGateQueryHandler : IRequestHandler<GetByIdTenantGateQuery, GetByIdTenantGateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGateRepository _gateRepository;
        private readonly GateBusinessRules _gateBusinessRules;

        public GetByIdTenantGateQueryHandler(IMapper mapper, IGateRepository gateRepository, GateBusinessRules gateBusinessRules)
        {
            _mapper = mapper;
            _gateRepository = gateRepository;
            _gateBusinessRules = gateBusinessRules;
        }

        public async Task<GetByIdTenantGateResponse> Handle(GetByIdTenantGateQuery request, CancellationToken cancellationToken)
        {
            Gate? gate = await _gateRepository.GetAsync(predicate: g => g.Id == request.Id, cancellationToken: cancellationToken);
            await _gateBusinessRules.GateShouldExistWhenSelected(gate);

            GetByIdTenantGateResponse response = _mapper.Map<GetByIdTenantGateResponse>(gate);
            return response;
        }
    }
}
