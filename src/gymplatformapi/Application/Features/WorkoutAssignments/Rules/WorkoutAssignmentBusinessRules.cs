using Application.Features.DietAssignments.Constants;
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

    public async Task MemberShouldExistWhenSelected(Member? member)
    {
        if (member == null)
            await throwBusinessException(WorkoutAssignmentsBusinessMessages.MemberNotExists);
    }

    public async Task WorkoutTemplateShouldExistWhenSelected(WorkoutTemplate? workoutTemplate)
    {
        if (workoutTemplate == null)
            await throwBusinessException(WorkoutAssignmentsBusinessMessages.WorkoutTemplateNotExists);
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

    public async Task AllMembersShouldExistInCurrentTenant(ICollection<Guid> requestedMemberIds, IList<Member>? foundMembers)
    {
        if (foundMembers is null)
            await throwBusinessException(DietAssignmentsBusinessMessages.MemberNotExists);

        HashSet<Guid> requestedSet = requestedMemberIds.Distinct().ToHashSet();

        HashSet<Guid> foundSet = foundMembers.Select(x => x.Id).ToHashSet();

        if (!requestedSet.SetEquals(foundSet))
            await throwBusinessException(DietAssignmentsBusinessMessages.MemberNotExists);
    }
}
