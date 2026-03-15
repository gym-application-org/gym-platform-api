using Application.Features.WorkoutTemplates.Commands.Create.Dtos;
using Application.Features.WorkoutTemplates.Constants;
using Application.Features.WorkoutTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using static Application.Features.WorkoutTemplates.Constants.WorkoutTemplatesOperationClaims;

namespace Application.Features.WorkoutTemplates.Commands.Create;

public class CreateWorkoutTemplateCommand
    : IRequest<CreatedWorkoutTemplateResponse>,
        ISecuredRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public DifficultyLevel Level { get; set; }
    public bool IsActive { get; set; }

    public ICollection<WorkoutTemplateDayDto> Days { get; set; } = new List<WorkoutTemplateDayDto>();

    public string[] Roles => [GeneralOperationClaims.Staff, GeneralOperationClaims.Owner];

    public class CreateWorkoutTemplateCommandHandler : IRequestHandler<CreateWorkoutTemplateCommand, CreatedWorkoutTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateRepository _workoutTemplateRepository;
        private readonly WorkoutTemplateBusinessRules _workoutTemplateBusinessRules;
        private readonly ICurrentTenant _currentTenant;

        public CreateWorkoutTemplateCommandHandler(
            IMapper mapper,
            IWorkoutTemplateRepository workoutTemplateRepository,
            WorkoutTemplateBusinessRules workoutTemplateBusinessRules,
            ICurrentTenant currentTenant
        )
        {
            _mapper = mapper;
            _workoutTemplateRepository = workoutTemplateRepository;
            _workoutTemplateBusinessRules = workoutTemplateBusinessRules;
            _currentTenant = currentTenant;
        }

        public async Task<CreatedWorkoutTemplateResponse> Handle(CreateWorkoutTemplateCommand request, CancellationToken cancellationToken)
        {
            Guid tenantId = _currentTenant.TenantId!.Value;

            WorkoutTemplate workoutTemplate =
                new(
                    tenantId: tenantId,
                    name: request.Name,
                    description: request.Description,
                    level: request.Level,
                    isActive: request.IsActive
                );

            foreach (var dayDto in request.Days)
            {
                WorkoutTemplateDay day =
                    new(tenantId: tenantId, workoutTemplateId: 0, dayNumber: dayDto.DayNumber, title: dayDto.Title)
                    {
                        Notes = dayDto.Notes
                    };

                foreach (var exerciseDto in dayDto.Exercises)
                {
                    WorkoutTemplateDayExercise exercise =
                        new(
                            tenantId: tenantId,
                            workoutTemplateDayId: 0,
                            exerciseId: exerciseDto.ExerciseId,
                            order: exerciseDto.Order,
                            sets: exerciseDto.Sets,
                            reps: exerciseDto.Reps,
                            weightKg: exerciseDto.WeightKg,
                            restSeconds: exerciseDto.RestSeconds,
                            tempo: exerciseDto.Tempo,
                            note: exerciseDto.Note
                        );

                    day.Exercises.Add(exercise);
                }

                workoutTemplate.Days.Add(day);
            }

            await _workoutTemplateRepository.AddAsync(workoutTemplate);

            CreatedWorkoutTemplateResponse response = _mapper.Map<CreatedWorkoutTemplateResponse>(workoutTemplate);
            return response;
        }
    }
}
