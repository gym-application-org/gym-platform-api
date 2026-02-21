using Application.Features.Exercises.Constants;
using Application.Features.Exercises.Constants;
using Application.Features.Exercises.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using MediatR;
using static Application.Features.Exercises.Constants.ExercisesOperationClaims;

namespace Application.Features.Exercises.Commands.Delete;

public class DeleteExerciseCommand
    : IRequest<DeletedExerciseResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetExercises"];

    public class DeleteExerciseCommandHandler : IRequestHandler<DeleteExerciseCommand, DeletedExerciseResponse>
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ExerciseBusinessRules _exerciseBusinessRules;

        public DeleteExerciseCommandHandler(
            IMapper mapper,
            IExerciseRepository exerciseRepository,
            ExerciseBusinessRules exerciseBusinessRules
        )
        {
            _mapper = mapper;
            _exerciseRepository = exerciseRepository;
            _exerciseBusinessRules = exerciseBusinessRules;
        }

        public async Task<DeletedExerciseResponse> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            Exercise? exercise = await _exerciseRepository.GetAsync(
                predicate: e => e.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _exerciseBusinessRules.ExerciseShouldExistWhenSelected(exercise);

            await _exerciseRepository.DeleteAsync(exercise!);

            DeletedExerciseResponse response = _mapper.Map<DeletedExerciseResponse>(exercise);
            return response;
        }
    }
}
