using Application.Features.WorkoutTemplateDayExercises.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutTemplateDayExercises.Constants.WorkoutTemplateDayExercisesOperationClaims;

namespace Application.Features.WorkoutTemplateDayExercises.Queries.GetList;

public class GetListWorkoutTemplateDayExerciseQuery
    : IRequest<GetListResponse<GetListWorkoutTemplateDayExerciseListItemDto>>,
        ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListWorkoutTemplateDayExerciseQueryHandler
        : IRequestHandler<GetListWorkoutTemplateDayExerciseQuery, GetListResponse<GetListWorkoutTemplateDayExerciseListItemDto>>
    {
        private readonly IWorkoutTemplateDayExerciseRepository _workoutTemplateDayExerciseRepository;
        private readonly IMapper _mapper;

        public GetListWorkoutTemplateDayExerciseQueryHandler(
            IWorkoutTemplateDayExerciseRepository workoutTemplateDayExerciseRepository,
            IMapper mapper
        )
        {
            _workoutTemplateDayExerciseRepository = workoutTemplateDayExerciseRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListWorkoutTemplateDayExerciseListItemDto>> Handle(
            GetListWorkoutTemplateDayExerciseQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<WorkoutTemplateDayExercise> workoutTemplateDayExercises = await _workoutTemplateDayExerciseRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListWorkoutTemplateDayExerciseListItemDto> response = _mapper.Map<
                GetListResponse<GetListWorkoutTemplateDayExerciseListItemDto>
            >(workoutTemplateDayExercises);
            return response;
        }
    }
}
