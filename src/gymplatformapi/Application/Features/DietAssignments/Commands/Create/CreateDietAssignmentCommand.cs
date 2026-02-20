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

namespace Application.Features.DietAssignments.Commands.Create;

public class CreateDietAssignmentCommand
    : IRequest<CreatedDietAssignmentResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    public int DietTemplateId { get; set; }

    public string[] Roles => [Admin, Write, DietAssignmentsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietAssignments"];

    public class CreateDietAssignmentCommandHandler : IRequestHandler<CreateDietAssignmentCommand, CreatedDietAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietAssignmentRepository _dietAssignmentRepository;
        private readonly DietAssignmentBusinessRules _dietAssignmentBusinessRules;

        public CreateDietAssignmentCommandHandler(
            IMapper mapper,
            IDietAssignmentRepository dietAssignmentRepository,
            DietAssignmentBusinessRules dietAssignmentBusinessRules
        )
        {
            _mapper = mapper;
            _dietAssignmentRepository = dietAssignmentRepository;
            _dietAssignmentBusinessRules = dietAssignmentBusinessRules;
        }

        public async Task<CreatedDietAssignmentResponse> Handle(CreateDietAssignmentCommand request, CancellationToken cancellationToken)
        {
            DietAssignment dietAssignment = _mapper.Map<DietAssignment>(request);

            await _dietAssignmentRepository.AddAsync(dietAssignment);

            CreatedDietAssignmentResponse response = _mapper.Map<CreatedDietAssignmentResponse>(dietAssignment);
            return response;
        }
    }
}
