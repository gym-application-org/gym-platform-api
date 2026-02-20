using Application.Features.Exercises.Constants;
using Application.Features.Exercises.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Exercises.Constants.ExercisesOperationClaims;

namespace Application.Features.Exercises.Queries.GetById;

public class GetByIdExerciseQuery : IRequest<GetByIdExerciseResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdExerciseQueryHandler : IRequestHandler<GetByIdExerciseQuery, GetByIdExerciseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ExerciseBusinessRules _exerciseBusinessRules;

        public GetByIdExerciseQueryHandler(
            IMapper mapper,
            IExerciseRepository exerciseRepository,
            ExerciseBusinessRules exerciseBusinessRules
        )
        {
            _mapper = mapper;
            _exerciseRepository = exerciseRepository;
            _exerciseBusinessRules = exerciseBusinessRules;
        }

        public async Task<GetByIdExerciseResponse> Handle(GetByIdExerciseQuery request, CancellationToken cancellationToken)
        {
            Exercise? exercise = await _exerciseRepository.GetAsync(
                predicate: e => e.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _exerciseBusinessRules.ExerciseShouldExistWhenSelected(exercise);

            GetByIdExerciseResponse response = _mapper.Map<GetByIdExerciseResponse>(exercise);
            return response;
        }
    }
}
