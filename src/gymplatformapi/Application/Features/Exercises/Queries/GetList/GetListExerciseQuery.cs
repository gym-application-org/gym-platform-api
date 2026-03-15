using Application.Features.Exercises.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Exercises.Constants.ExercisesOperationClaims;

namespace Application.Features.Exercises.Queries.GetList;

public class GetListExerciseQuery : IRequest<GetListResponse<GetListExerciseListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles =>
        [GeneralOperationClaims.Admin, GeneralOperationClaims.Owner, GeneralOperationClaims.Staff, GeneralOperationClaims.Member];

    public class GetListExerciseQueryHandler : IRequestHandler<GetListExerciseQuery, GetListResponse<GetListExerciseListItemDto>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public GetListExerciseQueryHandler(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListExerciseListItemDto>> Handle(
            GetListExerciseQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Exercise> exercises = await _exerciseRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListExerciseListItemDto> response = _mapper.Map<GetListResponse<GetListExerciseListItemDto>>(exercises);
            return response;
        }
    }
}
