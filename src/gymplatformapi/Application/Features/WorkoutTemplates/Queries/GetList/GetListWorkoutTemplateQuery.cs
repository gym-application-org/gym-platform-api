using Application.Features.WorkoutTemplates.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutTemplates.Constants.WorkoutTemplatesOperationClaims;

namespace Application.Features.WorkoutTemplates.Queries.GetList;

public class GetListWorkoutTemplateQuery : IRequest<GetListResponse<GetListWorkoutTemplateListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListWorkoutTemplates({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetWorkoutTemplates";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListWorkoutTemplateQueryHandler
        : IRequestHandler<GetListWorkoutTemplateQuery, GetListResponse<GetListWorkoutTemplateListItemDto>>
    {
        private readonly IWorkoutTemplateRepository _workoutTemplateRepository;
        private readonly IMapper _mapper;

        public GetListWorkoutTemplateQueryHandler(IWorkoutTemplateRepository workoutTemplateRepository, IMapper mapper)
        {
            _workoutTemplateRepository = workoutTemplateRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListWorkoutTemplateListItemDto>> Handle(
            GetListWorkoutTemplateQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<WorkoutTemplate> workoutTemplates = await _workoutTemplateRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListWorkoutTemplateListItemDto> response = _mapper.Map<GetListResponse<GetListWorkoutTemplateListItemDto>>(
                workoutTemplates
            );
            return response;
        }
    }
}
