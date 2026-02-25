using Application.Features.DietAssignments.Constants;
using Application.Features.DietAssignments.Queries.GetMyDietAssignemnts;
using Application.Features.DietAssignments.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.DietAssignments.Constants.DietAssignmentsOperationClaims;

namespace Application.Features.DietAssignments.Queries.GetMyDietAssignments;

public class GetMyDietAssignmentsListQuery
    : IRequest<GetListResponse<GetMyDietAssignmentsListItemDto>>,
        ISecuredRequest,
        ICachableRequest,
        ITenantRequest
{
    public PageRequest PageRequest { get; set; }

    public AssignmentStatus? Status { get; set; }

    public string[] Roles => [GeneralOperationClaims.Member];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListDietAssignments({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetDietAssignments";
    public TimeSpan? SlidingExpiration { get; }

    public class GetMyDietAssignmentsListQueryHandler
        : IRequestHandler<GetMyDietAssignmentsListQuery, GetListResponse<GetMyDietAssignmentsListItemDto>>
    {
        private readonly IDietAssignmentRepository _dietAssignmentRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IMemberService _memberService;
        private readonly DietAssignmentBusinessRules _dietAssignmentBusinessRules;

        public GetMyDietAssignmentsListQueryHandler(
            IDietAssignmentRepository dietAssignmentRepository,
            IMapper mapper,
            ICurrentUser currentUser,
            IMemberService memberService,
            DietAssignmentBusinessRules dietAssignmentBusinessRules
        )
        {
            _dietAssignmentRepository = dietAssignmentRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _memberService = memberService;
            _dietAssignmentBusinessRules = dietAssignmentBusinessRules;
        }

        public async Task<GetListResponse<GetMyDietAssignmentsListItemDto>> Handle(
            GetMyDietAssignmentsListQuery request,
            CancellationToken cancellationToken
        )
        {
            Member? member = await _memberService.GetAsync(x => x.UserId == _currentUser.UserId, cancellationToken: cancellationToken);
            await _dietAssignmentBusinessRules.MemberShouldExistWhenSelected(member);

            IPaginate<DietAssignment> dietAssignments = await _dietAssignmentRepository.GetListAsync(
                predicate: x => x.MemberId == member!.Id && (!request.Status.HasValue || x.Status == request.Status),
                orderBy: x => x.OrderByDescending(x => x.CreatedDate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetMyDietAssignmentsListItemDto> response = _mapper.Map<GetListResponse<GetMyDietAssignmentsListItemDto>>(
                dietAssignments
            );
            return response;
        }
    }
}
