using Application.Features.WorkoutAssignments.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.WorkoutAssignments.Constants.WorkoutAssignmentsOperationClaims;

namespace Application.Features.WorkoutAssignments.Queries.GetList;

public class GetListWorkoutAssignmentQuery : IRequest<GetListResponse<GetListWorkoutAssignmentListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public AssignmentStatus? Status { get; set; }

    public string[] Roles => [GeneralOperationClaims.Owner, GeneralOperationClaims.Staff];

    public class GetListWorkoutAssignmentQueryHandler
        : IRequestHandler<GetListWorkoutAssignmentQuery, GetListResponse<GetListWorkoutAssignmentListItemDto>>
    {
        private readonly IWorkoutAssignmentRepository _workoutAssignmentRepository;
        private readonly IMapper _mapper;

        public GetListWorkoutAssignmentQueryHandler(IWorkoutAssignmentRepository workoutAssignmentRepository, IMapper mapper)
        {
            _workoutAssignmentRepository = workoutAssignmentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListWorkoutAssignmentListItemDto>> Handle(
            GetListWorkoutAssignmentQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<WorkoutAssignment> workoutAssignments = await _workoutAssignmentRepository.GetListAsync(
                predicate: x =>
                    (!request.From.HasValue || request.From.Value <= x.StartDate)
                    && (!request.To.HasValue || request.To.Value >= x.EndDate)
                    && (!request.Status.HasValue || request.Status.Value == x.Status),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListWorkoutAssignmentListItemDto> response = _mapper.Map<
                GetListResponse<GetListWorkoutAssignmentListItemDto>
            >(workoutAssignments);
            return response;
        }
    }
}
