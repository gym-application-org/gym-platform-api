using Application.Features.Staffs.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Staffs.Rules;

public class StaffBusinessRules : BaseBusinessRules
{
    private readonly IStaffRepository _staffRepository;
    private readonly ILocalizationService _localizationService;

    public StaffBusinessRules(IStaffRepository staffRepository, ILocalizationService localizationService)
    {
        _staffRepository = staffRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, StaffsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task StaffShouldExistWhenSelected(Staff? staff)
    {
        if (staff == null)
            await throwBusinessException(StaffsBusinessMessages.StaffNotExists);
    }

    public async Task StaffIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Staff? staff = await _staffRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StaffShouldExistWhenSelected(staff);
    }
}
