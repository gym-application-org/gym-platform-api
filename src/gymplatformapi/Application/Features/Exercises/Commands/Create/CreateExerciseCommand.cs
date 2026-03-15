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

namespace Application.Features.Exercises.Commands.Create;

public class CreateExerciseCommand : IRequest<CreatedExerciseResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? MuscleGroup { get; set; }
    public string? Equipment { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public string? VideoUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class CreateExerciseCommandHandler : IRequestHandler<CreateExerciseCommand, CreatedExerciseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ExerciseBusinessRules _exerciseBusinessRules;

        public CreateExerciseCommandHandler(
            IMapper mapper,
            IExerciseRepository exerciseRepository,
            ExerciseBusinessRules exerciseBusinessRules
        )
        {
            _mapper = mapper;
            _exerciseRepository = exerciseRepository;
            _exerciseBusinessRules = exerciseBusinessRules;
        }

        public async Task<CreatedExerciseResponse> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            Exercise exercise = _mapper.Map<Exercise>(request);

            await _exerciseRepository.AddAsync(exercise);

            CreatedExerciseResponse response = _mapper.Map<CreatedExerciseResponse>(exercise);
            return response;
        }
    }
}
