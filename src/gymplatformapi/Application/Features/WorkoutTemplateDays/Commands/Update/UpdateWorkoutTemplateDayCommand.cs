using Application.Features.WorkoutTemplateDays.Constants;
using Application.Features.WorkoutTemplateDays.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using static Application.Features.WorkoutTemplateDays.Constants.WorkoutTemplateDaysOperationClaims;

namespace Application.Features.WorkoutTemplateDays.Commands.Update;

public class UpdateWorkoutTemplateDayCommand
    : IRequest<UpdatedWorkoutTemplateDayResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public string Title { get; set; }
    public string? Notes { get; set; }
    public int WorkoutTemplateId { get; set; }

    public string[] Roles => [Admin, Write, WorkoutTemplateDaysOperationClaims.Update];

    public class UpdateWorkoutTemplateDayCommandHandler
        : IRequestHandler<UpdateWorkoutTemplateDayCommand, UpdatedWorkoutTemplateDayResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateDayRepository _workoutTemplateDayRepository;
        private readonly WorkoutTemplateDayBusinessRules _workoutTemplateDayBusinessRules;

        public UpdateWorkoutTemplateDayCommandHandler(
            IMapper mapper,
            IWorkoutTemplateDayRepository workoutTemplateDayRepository,
            WorkoutTemplateDayBusinessRules workoutTemplateDayBusinessRules
        )
        {
            _mapper = mapper;
            _workoutTemplateDayRepository = workoutTemplateDayRepository;
            _workoutTemplateDayBusinessRules = workoutTemplateDayBusinessRules;
        }

        public async Task<UpdatedWorkoutTemplateDayResponse> Handle(
            UpdateWorkoutTemplateDayCommand request,
            CancellationToken cancellationToken
        )
        {
            WorkoutTemplateDay? workoutTemplateDay = await _workoutTemplateDayRepository.GetAsync(
                predicate: wtd => wtd.Id == request.Id,
                cancellationToken: cancellationToken
            );
            await _workoutTemplateDayBusinessRules.WorkoutTemplateDayShouldExistWhenSelected(workoutTemplateDay);
            workoutTemplateDay = _mapper.Map(request, workoutTemplateDay);

            await _workoutTemplateDayRepository.UpdateAsync(workoutTemplateDay!);

            UpdatedWorkoutTemplateDayResponse response = _mapper.Map<UpdatedWorkoutTemplateDayResponse>(workoutTemplateDay);
            return response;
        }
    }
}
