using Application.Features.Gates.Constants;
using Application.Features.Gates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.Gates.Constants.GatesOperationClaims;

namespace Application.Features.Gates.Commands.Create;

public class CreateGateCommand
    : IRequest<CreatedGateResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public string Name { get; set; }
    public string GateCode { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [Admin, Write, GatesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetGates"];

    public class CreateGateCommandHandler : IRequestHandler<CreateGateCommand, CreatedGateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGateRepository _gateRepository;
        private readonly GateBusinessRules _gateBusinessRules;

        public CreateGateCommandHandler(IMapper mapper, IGateRepository gateRepository, GateBusinessRules gateBusinessRules)
        {
            _mapper = mapper;
            _gateRepository = gateRepository;
            _gateBusinessRules = gateBusinessRules;
        }

        public async Task<CreatedGateResponse> Handle(CreateGateCommand request, CancellationToken cancellationToken)
        {
            Gate gate = _mapper.Map<Gate>(request);

            await _gateRepository.AddAsync(gate);

            CreatedGateResponse response = _mapper.Map<CreatedGateResponse>(gate);
            return response;
        }
    }
}
