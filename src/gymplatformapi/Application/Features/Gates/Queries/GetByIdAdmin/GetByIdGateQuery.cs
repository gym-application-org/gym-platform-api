using Application.Features.Gates.Constants;
using Application.Features.Gates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;

namespace Application.Features.Gates.Queries.GetByIdAdmin;

public class GetByIdGateQuery : IRequest<GetByIdGateResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class GetByIdGateQueryHandler : IRequestHandler<GetByIdGateQuery, GetByIdGateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGateRepository _gateRepository;
        private readonly GateBusinessRules _gateBusinessRules;

        public GetByIdGateQueryHandler(IMapper mapper, IGateRepository gateRepository, GateBusinessRules gateBusinessRules)
        {
            _mapper = mapper;
            _gateRepository = gateRepository;
            _gateBusinessRules = gateBusinessRules;
        }

        public async Task<GetByIdGateResponse> Handle(GetByIdGateQuery request, CancellationToken cancellationToken)
        {
            Gate? gate = await _gateRepository.GetAsync(
                predicate: g => g.Id == request.Id,
                withDeleted: true,
                cancellationToken: cancellationToken
            );
            await _gateBusinessRules.GateShouldExistWhenSelected(gate);

            GetByIdGateResponse response = _mapper.Map<GetByIdGateResponse>(gate);
            return response;
        }
    }
}
