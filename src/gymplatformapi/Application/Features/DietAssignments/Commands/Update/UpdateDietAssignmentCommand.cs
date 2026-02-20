using Application.Features.DietAssignments.Constants;
using Application.Features.DietAssignments.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.DietAssignments.Constants.DietAssignmentsOperationClaims;

namespace Application.Features.DietAssignments.Commands.Update;

public class UpdateDietAssignmentCommand
    : IRequest<UpdatedDietAssignmentResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    public int DietTemplateId { get; set; }

    public string[] Roles => [Admin, Write, DietAssignmentsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietAssignments"];

    public class UpdateDietAssignmentCommandHandler : IRequestHandler<UpdateDietAssignmentCommand, UpdatedDietAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietAssignmentRepository _dietAssignmentRepository;
        private readonly DietAssignmentBusinessRules _dietAssignmentBusinessRules;

        public UpdateDietAssignmentCommandHandler(
            IMapper mapper,
            IDietAssignmentRepository dietAssignmentRepository,
            DietAssignmentBusinessRules dietAssignmentBusinessRules
        )
        {
            _mapper = mapper;
            _dietAssignmentRepository = dietAssignmentRepository;
            _dietAssignmentBusinessRules = dietAssignmentBusinessRules;
        }

        public async Task<UpdatedDietAssignmentResponse> Handle(UpdateDietAssignmentCommand request, CancellationToken cancellationToken)
        {
            DietAssignment? dietAssignment = await _dietAssignmentRepository.GetAsync(
                predicate: da => da.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _dietAssignmentBusinessRules.DietAssignmentShouldExistWhenSelected(dietAssignment);
            dietAssignment = _mapper.Map(request, dietAssignment);

            await _dietAssignmentRepository.UpdateAsync(dietAssignment!);

            UpdatedDietAssignmentResponse response = _mapper.Map<UpdatedDietAssignmentResponse>(dietAssignment);
            return response;
        }
    }
}
