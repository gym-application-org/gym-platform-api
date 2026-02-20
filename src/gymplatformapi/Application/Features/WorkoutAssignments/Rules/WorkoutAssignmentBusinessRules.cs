using Application.Features.WorkoutAssignments.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.WorkoutAssignments.Rules;

public class WorkoutAssignmentBusinessRules : BaseBusinessRules
{
    private readonly IWorkoutAssignmentRepository _workoutAssignmentRepository;
    private readonly ILocalizationService _localizationService;

    public WorkoutAssignmentBusinessRules(
        IWorkoutAssignmentRepository workoutAssignmentRepository,
        ILocalizationService localizationService
    )
    {
        _workoutAssignmentRepository = workoutAssignmentRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, WorkoutAssignmentsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task WorkoutAssignmentShouldExistWhenSelected(WorkoutAssignment? workoutAssignment)
    {
        if (workoutAssignment == null)
            await throwBusinessException(WorkoutAssignmentsBusinessMessages.WorkoutAssignmentNotExists);
    }

    public async Task WorkoutAssignmentIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        WorkoutAssignment? workoutAssignment = await _workoutAssignmentRepository.GetAsync(
            predicate: wa => wa.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await WorkoutAssignmentShouldExistWhenSelected(workoutAssignment);
    }
}
