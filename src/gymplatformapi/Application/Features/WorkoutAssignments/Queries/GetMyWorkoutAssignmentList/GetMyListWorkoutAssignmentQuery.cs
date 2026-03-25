using Application.Features.WorkoutAssignments.Constants;
using Application.Features.WorkoutAssignments.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.WorkoutAssignments.Constants.WorkoutAssignmentsOperationClaims;

namespace Application.Features.WorkoutAssignments.Queries.GetMyWorkoutAssignmentList;

public class GetMyListWorkoutAssignmentQuery : IRequest<GetListResponse<GetMyListWorkoutAssignmentListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public AssignmentStatus? Status { get; set; }

    public string[] Roles => [GeneralOperationClaims.Member];

    public class GetMyListWorkoutAssignmentQueryHandler
        : IRequestHandler<GetMyListWorkoutAssignmentQuery, GetListResponse<GetMyListWorkoutAssignmentListItemDto>>
    {
        private readonly IWorkoutAssignmentRepository _workoutAssignmentRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IMemberService _memberService;
        private readonly WorkoutAssignmentBusinessRules _workoutAssignmentBusinessRules;

        public GetMyListWorkoutAssignmentQueryHandler(
            IWorkoutAssignmentRepository workoutAssignmentRepository,
            IMapper mapper,
            ICurrentUser currentUser,
            IMemberService memberService,
            WorkoutAssignmentBusinessRules workoutAssignmentBusinessRules
        )
        {
            _workoutAssignmentRepository = workoutAssignmentRepository;
            _mapper = mapper;
            _currentUser = currentUser;
            _memberService = memberService;
            _workoutAssignmentBusinessRules = workoutAssignmentBusinessRules;
        }

        public async Task<GetListResponse<GetMyListWorkoutAssignmentListItemDto>> Handle(
            GetMyListWorkoutAssignmentQuery request,
            CancellationToken cancellationToken
        )
        {
            Member? member = await _memberService.GetAsync(x => x.UserId == _currentUser.UserId!, cancellationToken: cancellationToken);
            await _workoutAssignmentBusinessRules.MemberShouldExistWhenSelected(member);

            IPaginate<WorkoutAssignment> workoutAssignments = await _workoutAssignmentRepository.GetListAsync(
                predicate: x =>
                    x.MemberId == member!.Id
                    && (!request.From.HasValue || request.From.Value <= x.StartDate)
                    && (!request.To.HasValue || ((!x.EndDate.HasValue) || (x.EndDate.HasValue && x.EndDate <= request.To)))
                    && (!request.Status.HasValue || request.Status.Value == x.Status),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetMyListWorkoutAssignmentListItemDto> response = _mapper.Map<
                GetListResponse<GetMyListWorkoutAssignmentListItemDto>
            >(workoutAssignments);
            return response;
        }
    }
}
