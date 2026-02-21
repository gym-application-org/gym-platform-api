using Application.Features.Gates.Constants;
using Application.Features.Gates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Gates.Constants.GatesOperationClaims;

namespace Application.Features.Gates.Commands.Update;

public class UpdateGateCommand
    : IRequest<UpdatedGateResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public Guid TenantId { get; set; }
    public string Name { get; set; }
    public string GateCode { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetGates"];

    public class UpdateGateCommandHandler : IRequestHandler<UpdateGateCommand, UpdatedGateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGateRepository _gateRepository;
        private readonly GateBusinessRules _gateBusinessRules;

        public UpdateGateCommandHandler(IMapper mapper, IGateRepository gateRepository, GateBusinessRules gateBusinessRules)
        {
            _mapper = mapper;
            _gateRepository = gateRepository;
            _gateBusinessRules = gateBusinessRules;
        }

        public async Task<UpdatedGateResponse> Handle(UpdateGateCommand request, CancellationToken cancellationToken)
        {
            Gate? gate = await _gateRepository.GetAsync(predicate: g => g.Id == request.Id, cancellationToken: cancellationToken);
            await _gateBusinessRules.GateShouldExistWhenSelected(gate);
            gate = _mapper.Map(request, gate);

            await _gateRepository.UpdateAsync(gate!);

            UpdatedGateResponse response = _mapper.Map<UpdatedGateResponse>(gate);
            return response;
        }
    }
}
