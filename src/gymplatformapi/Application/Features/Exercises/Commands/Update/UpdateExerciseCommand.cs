using Application.Features.Exercises.Constants;
using Application.Features.Exercises.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.Exercises.Constants.ExercisesOperationClaims;

namespace Application.Features.Exercises.Commands.Update;

public class UpdateExerciseCommand : IRequest<UpdatedExerciseResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? MuscleGroup { get; set; }
    public string? Equipment { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public string? VideoUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class UpdateExerciseCommandHandler : IRequestHandler<UpdateExerciseCommand, UpdatedExerciseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ExerciseBusinessRules _exerciseBusinessRules;

        public UpdateExerciseCommandHandler(
            IMapper mapper,
            IExerciseRepository exerciseRepository,
            ExerciseBusinessRules exerciseBusinessRules
        )
        {
            _mapper = mapper;
            _exerciseRepository = exerciseRepository;
            _exerciseBusinessRules = exerciseBusinessRules;
        }

        public async Task<UpdatedExerciseResponse> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
        {
            Exercise? exercise = await _exerciseRepository.GetAsync(
                predicate: e => e.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _exerciseBusinessRules.ExerciseShouldExistWhenSelected(exercise);
            exercise = _mapper.Map(request, exercise);

            await _exerciseRepository.UpdateAsync(exercise!);

            UpdatedExerciseResponse response = _mapper.Map<UpdatedExerciseResponse>(exercise);
            return response;
        }
    }
}
