using Application.Features.WorkoutAssignments.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutAssignments.Constants.WorkoutAssignmentsOperationClaims;

namespace Application.Features.WorkoutAssignments.Queries.GetList;

public class GetListWorkoutAssignmentQuery
    : IRequest<GetListResponse<GetListWorkoutAssignmentListItemDto>>,
        ISecuredRequest,
        ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListWorkoutAssignments({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetWorkoutAssignments";
    public TimeSpan? SlidingExpiration { get; }

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
