using Application.Features.DietAssignments.Constants;
using Application.Features.DietAssignments.Rules;
using Application.Services.DietTemplates;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.DietAssignments.Constants.DietAssignmentsOperationClaims;

namespace Application.Features.DietAssignments.Commands.CreateBulk;

public class CreateBulkDietAssignmentCommand
    : IRequest<CreatedBulkDietAssignmentResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }
    public ICollection<Guid> MemberIds { get; set; } = new List<Guid>();
    public int DietTemplateId { get; set; }

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetDietAssignments"];

    public class CreateDietAssignmentCommandHandler : IRequestHandler<CreateBulkDietAssignmentCommand, CreatedBulkDietAssignmentResponse>
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

        public async Task<CreatedBulkDietAssignmentResponse> Handle(
            CreateBulkDietAssignmentCommand request,
            CancellationToken cancellationToken
        )
        {
            Guid tenantId = _currentTenant.TenantId!.Value;

            List<Guid> distinctMemberIds = request.MemberIds.Distinct().ToList();

            var members = await _memberService.GetListAsync(
                predicate: x => distinctMemberIds.Contains(x.Id),
                index: 0,
                size: distinctMemberIds.Count,
                cancellationToken: cancellationToken
            );

            await _dietAssignmentBusinessRules.AllMembersShouldExistInCurrentTenant(
                requestedMemberIds: distinctMemberIds,
                foundMembers: members?.Items
            );

            DietTemplate? dietTemplate = await _dietTemplateService.GetAsync(
                x => x.Id == request.DietTemplateId,
                cancellationToken: cancellationToken
            );
            await _dietAssignmentBusinessRules.DietTemplateShouldExistWhenSelected(dietTemplate);

            List<DietAssignment> assignments = new();

            foreach (Member member in members!.Items)
            {
                DietAssignment dietAssignment =
                    new(
                        tenantId: tenantId,
                        memberId: member.Id,
                        dietTemplateId: request.DietTemplateId,
                        startDate: request.StartDate,
                        endDate: request.EndDate
                    )
                    {
                        Status = request.Status
                    };

                assignments.Add(dietAssignment);
            }

            await _dietAssignmentRepository.AddRangeAsync(assignments);

            return new CreatedBulkDietAssignmentResponse
            {
                DietTemplateId = request.DietTemplateId,
                CreatedCount = assignments.Count,
                AssignmentIds = assignments.Select(x => x.Id).ToList()
            };
        }
    }
}
