using Application.Features.DietAssignments.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.DietAssignments.Rules;

public class DietAssignmentBusinessRules : BaseBusinessRules
{
    private readonly IDietAssignmentRepository _dietAssignmentRepository;
    private readonly ILocalizationService _localizationService;

    public DietAssignmentBusinessRules(IDietAssignmentRepository dietAssignmentRepository, ILocalizationService localizationService)
    {
        _dietAssignmentRepository = dietAssignmentRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, DietAssignmentsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task DietAssignmentShouldExistWhenSelected(DietAssignment? dietAssignment)
    {
        if (dietAssignment == null)
            await throwBusinessException(DietAssignmentsBusinessMessages.DietAssignmentNotExists);
    }

    public async Task DietAssignmentIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        DietAssignment? dietAssignment = await _dietAssignmentRepository.GetAsync(
            predicate: da => da.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DietAssignmentShouldExistWhenSelected(dietAssignment);
    }
}
