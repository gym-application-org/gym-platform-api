using Application.Features.DietAssignments.Constants;
using Application.Features.DietAssignments.Rules;
using Application.Services.DietTemplates;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.DietAssignments.Constants.DietAssignmentsOperationClaims;

namespace Application.Features.DietAssignments.Commands.Create;

public class CreateDietAssignmentCommand
    : IRequest<CreatedDietAssignmentResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public Guid MemberId { get; set; }
    public int DietTemplateId { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class CreateDietAssignmentCommandHandler : IRequestHandler<CreateDietAssignmentCommand, CreatedDietAssignmentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDietAssignmentRepository _dietAssignmentRepository;
        private readonly DietAssignmentBusinessRules _dietAssignmentBusinessRules;
        private readonly ICurrentTenant _currentTenant;
        private readonly IMemberService _memberService;
        private readonly IDietTemplateService _dietTemplateService;

        public CreateDietAssignmentCommandHandler(
            IMapper mapper,
            IDietAssignmentRepository dietAssignmentRepository,
            DietAssignmentBusinessRules dietAssignmentBusinessRules,
            ICurrentTenant currentTenant,
            IMemberService memberService,
            IDietTemplateService dietTemplateService
        )
        {
            _mapper = mapper;
            _dietAssignmentRepository = dietAssignmentRepository;
            _dietAssignmentBusinessRules = dietAssignmentBusinessRules;
            _currentTenant = currentTenant;
            _memberService = memberService;
            _dietTemplateService = dietTemplateService;
        }

        public async Task<CreatedDietAssignmentResponse> Handle(CreateDietAssignmentCommand request, CancellationToken cancellationToken)
        {
            Member? member = await _memberService.GetAsync(x => x.Id == request.MemberId, cancellationToken: cancellationToken);
            await _dietAssignmentBusinessRules.MemberShouldExistWhenSelected(member);

            DietTemplate? dietTemplate = await _dietTemplateService.GetAsync(
                x => x.Id == request.DietTemplateId,
                cancellationToken: cancellationToken
            );
            await _dietAssignmentBusinessRules.DietTemplateShouldExistWhenSelected(dietTemplate);

            DietAssignment dietAssignment = _mapper.Map<DietAssignment>(request);
            dietAssignment.TenantId = _currentTenant.TenantId!.Value;

            await _dietAssignmentRepository.AddAsync(dietAssignment);

            CreatedDietAssignmentResponse response = _mapper.Map<CreatedDietAssignmentResponse>(dietAssignment);
            return response;
        }
    }
}
