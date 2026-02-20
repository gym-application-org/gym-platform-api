using Application.Features.WorkoutTemplateDays.Constants;
using Application.Features.WorkoutTemplateDays.Constants;
using Application.Features.WorkoutTemplateDays.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutTemplateDays.Constants.WorkoutTemplateDaysOperationClaims;

namespace Application.Features.WorkoutTemplateDays.Commands.Delete;

public class DeleteWorkoutTemplateDayCommand
    : IRequest<DeletedWorkoutTemplateDayResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => [Admin, Write, WorkoutTemplateDaysOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWorkoutTemplateDays"];

    public class DeleteWorkoutTemplateDayCommandHandler
        : IRequestHandler<DeleteWorkoutTemplateDayCommand, DeletedWorkoutTemplateDayResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateDayRepository _workoutTemplateDayRepository;
        private readonly WorkoutTemplateDayBusinessRules _workoutTemplateDayBusinessRules;

        public DeleteWorkoutTemplateDayCommandHandler(
            IMapper mapper,
            IWorkoutTemplateDayRepository workoutTemplateDayRepository,
            WorkoutTemplateDayBusinessRules workoutTemplateDayBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateDayRepository = workoutTemplateDayRepository;
            _workoutTemplateDayBusinessRules = workoutTemplateDayBusinessRules;
        }

        public async Task<DeletedWorkoutTemplateDayResponse> Handle(
            DeleteWorkoutTemplateDayCommand request,
            CancellationToken cancellationToken
        )
        {
            WorkoutTemplateDay? workoutTemplateDay = await _workoutTemplateDayRepository.GetAsync(
                predicate: wtd => wtd.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutTemplateDayBusinessRules.WorkoutTemplateDayShouldExistWhenSelected(workoutTemplateDay);

            await _workoutTemplateDayRepository.DeleteAsync(workoutTemplateDay!);

            DeletedWorkoutTemplateDayResponse response = _mapper.Map<DeletedWorkoutTemplateDayResponse>(workoutTemplateDay);
            return response;
        }
    }
}
