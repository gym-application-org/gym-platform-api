using Application.Features.WorkoutTemplates.Commands.Create.Dtos;
using Application.Features.WorkoutTemplates.Commands.Update.Dtos;
using Application.Features.WorkoutTemplates.Constants;
using Application.Features.WorkoutTemplates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Abstractions.Security;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.WorkoutTemplates.Constants.WorkoutTemplatesOperationClaims;

namespace Application.Features.WorkoutTemplates.Commands.Update;

public class UpdateWorkoutTemplateCommand
    : IRequest<UpdatedWorkoutTemplateResponse>,
        ISecuredRequest,
        ICacheRemoverRequest,
        ILoggableRequest,
        ITransactionalRequest,
        ITenantRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DifficultyLevel Level { get; set; }
    public bool IsActive { get; set; }

    public ICollection<UpdateWorkoutTemplateDayDto> Days { get; set; } = new List<UpdateWorkoutTemplateDayDto>();

    public string[] Roles => [GeneralOperationClaims.Owner, GeneralOperationClaims.Staff];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetWorkoutTemplates"];

    public class UpdateWorkoutTemplateCommandHandler : IRequestHandler<UpdateWorkoutTemplateCommand, UpdatedWorkoutTemplateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutTemplateRepository _workoutTemplateRepository;
        private readonly WorkoutTemplateBusinessRules _workoutTemplateBusinessRules;
        private readonly ICurrentTenant _currentTenant;

        public UpdateWorkoutTemplateCommandHandler(
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

        public async Task<UpdatedWorkoutTemplateResponse> Handle(UpdateWorkoutTemplateCommand request, CancellationToken cancellationToken)
        {
            Guid tenantId = _currentTenant.TenantId!.Value;

            WorkoutTemplate? workoutTemplate = await _workoutTemplateRepository.GetAsync(
                predicate: wt => wt.Id == request.Id,
                include: q => q.Include(x => x.Days).ThenInclude(d => d.Exercises),
                enableTracking: true,
                cancellationToken: cancellationToken
            );
            await _workoutTemplateBusinessRules.WorkoutTemplateShouldExistWhenSelected(workoutTemplate);

            workoutTemplate!.Name = request.Name;
            workoutTemplate.Description = request.Description;
            workoutTemplate.Level = request.Level;
            workoutTemplate.IsActive = request.IsActive;

            workoutTemplate.Days.Clear();

            foreach (var dayDto in request.Days)
            {
                WorkoutTemplateDay day =
                    new(tenantId: tenantId, workoutTemplateId: workoutTemplate.Id, dayNumber: dayDto.DayNumber, title: dayDto.Title)
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

            await _workoutTemplateRepository.UpdateAsync(workoutTemplate!);

            UpdatedWorkoutTemplateResponse response = _mapper.Map<UpdatedWorkoutTemplateResponse>(workoutTemplate);
            return response;
        }
    }
}
