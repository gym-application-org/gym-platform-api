using System.Threading;
using Application.Features.DietAssignments.Queries.GetMyDietAssignmentById;
using Application.Features.DietAssignments.Rules;
using Application.Features.WorkoutAssignments.Constants;
using Application.Features.WorkoutAssignments.Rules;
using Application.Services.Members;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.WorkoutAssignments.Constants.WorkoutAssignmentsOperationClaims;

namespace Application.Features.WorkoutAssignments.Queries.GetMyWorkoutById;

public class GetMyByIdWorkoutQuery : IRequest<GetMyWorkoutByIdResponse>, ISecuredRequest, ITenantRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Member];

    public class GetByIdWorkoutAssignmentQueryHandler : IRequestHandler<GetMyByIdWorkoutQuery, GetMyWorkoutByIdResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutAssignmentRepository _workoutAssignmentRepository;
        private readonly WorkoutAssignmentBusinessRules _workoutAssignmentBusinessRules;
        private readonly IMemberService _memberService;
        private readonly ICurrentUser _currentUser;

        public GetByIdWorkoutAssignmentQueryHandler(
            IMapper mapper,
            IWorkoutAssignmentRepository workoutAssignmentRepository,
            WorkoutAssignmentBusinessRules workoutAssignmentBusinessRules,
            IMemberService memberService,
            ICurrentUser currentUser
        )
        {
            _mapper = mapper;
            _workoutAssignmentRepository = workoutAssignmentRepository;
            _workoutAssignmentBusinessRules = workoutAssignmentBusinessRules;
            _memberService = memberService;
            _currentUser = currentUser;
        }

        public async Task<GetMyWorkoutByIdResponse> Handle(GetMyByIdWorkoutQuery request, CancellationToken cancellationToken)
        {
            Member? member = await _memberService.GetAsync(x => x.UserId == _currentUser.UserId, cancellationToken: cancellationToken);
            await _workoutAssignmentBusinessRules.MemberShouldExistWhenSelected(member);

            WorkoutAssignment? workoutAssignment = await _workoutAssignmentRepository.GetAsync(
                predicate: wa => wa.Id == request.Id && wa.MemberId == member!.Id,
                include: q => q.Include(x => x.WorkoutTemplate).ThenInclude(t => t.Days).ThenInclude(d => d.Exercises),
                enableTracking: false,
                cancellationToken: cancellationToken
            );
            await _workoutAssignmentBusinessRules.WorkoutAssignmentShouldExistWhenSelected(workoutAssignment);

            GetMyWorkoutByIdResponse response = _mapper.Map<GetMyWorkoutByIdResponse>(workoutAssignment);
            return response;
        }
    }
}
