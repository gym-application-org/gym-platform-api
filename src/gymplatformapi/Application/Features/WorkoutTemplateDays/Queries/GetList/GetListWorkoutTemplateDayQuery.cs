using Application.Features.WorkoutTemplateDays.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutTemplateDays.Constants.WorkoutTemplateDaysOperationClaims;

namespace Application.Features.WorkoutTemplateDays.Queries.GetList;

public class GetListWorkoutTemplateDayQuery
    : IRequest<GetListResponse<GetListWorkoutTemplateDayListItemDto>>,
        ISecuredRequest,
        ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListWorkoutTemplateDays({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetWorkoutTemplateDays";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListWorkoutTemplateDayQueryHandler
        : IRequestHandler<GetListWorkoutTemplateDayQuery, GetListResponse<GetListWorkoutTemplateDayListItemDto>>
    {
        private readonly IWorkoutTemplateDayRepository _workoutTemplateDayRepository;
        private readonly IMapper _mapper;

        public GetListWorkoutTemplateDayQueryHandler(IWorkoutTemplateDayRepository workoutTemplateDayRepository, IMapper mapper)
        {
            _workoutTemplateDayRepository = workoutTemplateDayRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListWorkoutTemplateDayListItemDto>> Handle(
            GetListWorkoutTemplateDayQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<WorkoutTemplateDay> workoutTemplateDays = await _workoutTemplateDayRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListWorkoutTemplateDayListItemDto> response = _mapper.Map<
                GetListResponse<GetListWorkoutTemplateDayListItemDto>
            >(workoutTemplateDays);
            return response;
        }
    }
}
