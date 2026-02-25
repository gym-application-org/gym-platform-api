using Application.Features.DietAssignments.Constants;
using Application.Features.DietAssignments.Constants;
using Application.Features.DietAssignments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.DietAssignments.Constants.DietAssignmentsOperationClaims;

namespace Application.Features.DietAssignments.Commands.Delete;

public class DeleteDietAssignmentCommand
    : IRequest<DeletedDietAssignmentResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Owner, GeneralOperationClaims.Staff];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietAssignments"];

    public class DeleteDietAssignmentCommandHandler : IRequestHandler<DeleteDietAssignmentCommand, DeletedDietAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietAssignmentRepository _dietAssignmentRepository;
        private readonly DietAssignmentBusinessRules _dietAssignmentBusinessRules;

        public DeleteDietAssignmentCommandHandler(
            IMapper mapper,
            IDietAssignmentRepository dietAssignmentRepository,
            DietAssignmentBusinessRules dietAssignmentBusinessRules
        )
        {
            _mapper = mapper;
            _dietAssignmentRepository = dietAssignmentRepository;
            _dietAssignmentBusinessRules = dietAssignmentBusinessRules;
        }

        public async Task<DeletedDietAssignmentResponse> Handle(DeleteDietAssignmentCommand request, CancellationToken cancellationToken)
        {
            DietAssignment? dietAssignment = await _dietAssignmentRepository.GetAsync(
                predicate: da => da.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietAssignmentBusinessRules.DietAssignmentShouldExistWhenSelected(dietAssignment);

            await _dietAssignmentRepository.DeleteAsync(dietAssignment!);

            DeletedDietAssignmentResponse response = _mapper.Map<DeletedDietAssignmentResponse>(dietAssignment);
            return response;
        }
    }
}
