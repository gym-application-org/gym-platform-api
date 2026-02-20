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

namespace Application.Features.WorkoutTemplateDays.Commands.Create;

public class CreateWorkoutTemplateDayCommand
    : IRequest<CreatedWorkoutTemplateDayResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int DayNumber { get; set; }
    public string Title { get; set; }
    public string? Notes { get; set; }
    public int WorkoutTemplateId { get; set; }

    public string[] Roles => [Admin, Write, WorkoutTemplateDaysOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWorkoutTemplateDays"];

    public class CreateWorkoutTemplateDayCommandHandler
        : IRequestHandler<CreateWorkoutTemplateDayCommand, CreatedWorkoutTemplateDayResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateDayRepository _workoutTemplateDayRepository;
        private readonly WorkoutTemplateDayBusinessRules _workoutTemplateDayBusinessRules;

        public CreateWorkoutTemplateDayCommandHandler(
            IMapper mapper,
            IWorkoutTemplateDayRepository workoutTemplateDayRepository,
            WorkoutTemplateDayBusinessRules workoutTemplateDayBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateDayRepository = workoutTemplateDayRepository;
            _workoutTemplateDayBusinessRules = workoutTemplateDayBusinessRules;
        }

        public async Task<CreatedWorkoutTemplateDayResponse> Handle(
            CreateWorkoutTemplateDayCommand request,
            CancellationToken cancellationToken
        )
        {
            WorkoutTemplateDay workoutTemplateDay = _mapper.Map<WorkoutTemplateDay>(request);

            await _workoutTemplateDayRepository.AddAsync(workoutTemplateDay);

            CreatedWorkoutTemplateDayResponse response = _mapper.Map<CreatedWorkoutTemplateDayResponse>(workoutTemplateDay);
            return response;
        }
    }
}
