using Application.Features.Gates.Constants;
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

namespace Application.Features.Gates.Commands.Delete;

public class DeleteGateCommand
    : IRequest<DeletedGateResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, GatesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetGates"];

    public class DeleteGateCommandHandler : IRequestHandler<DeleteGateCommand, DeletedGateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGateRepository _gateRepository;
        private readonly GateBusinessRules _gateBusinessRules;

        public DeleteGateCommandHandler(IMapper mapper, IGateRepository gateRepository, GateBusinessRules gateBusinessRules)
        {
            _mapper = mapper;
            _gateRepository = gateRepository;
            _gateBusinessRules = gateBusinessRules;
        }

        public async Task<DeletedGateResponse> Handle(DeleteGateCommand request, CancellationToken cancellationToken)
        {
            Gate? gate = await _gateRepository.GetAsync(predicate: g => g.Id == request.Id, cancellationToken: cancellationToken);
            await _gateBusinessRules.GateShouldExistWhenSelected(gate);

            await _gateRepository.DeleteAsync(gate!);

            DeletedGateResponse response = _mapper.Map<DeletedGateResponse>(gate);
            return response;
        }
    }
}
