using Application.Features.Tenants.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Tenants.Rules;

public class TenantBusinessRules : BaseBusinessRules
{
    private readonly ITenantRepository _tenantRepository;
    private readonly ILocalizationService _localizationService;

    public TenantBusinessRules(ITenantRepository tenantRepository, ILocalizationService localizationService)
    {
        _tenantRepository = tenantRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, TenantsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task TenantShouldExistWhenSelected(Tenant? tenant)
    {
        if (tenant == null)
            await throwBusinessException(TenantsBusinessMessages.TenantNotExists);
    }

    public async Task MemberShouldBeActivated(Member member)
    {
        if (member.DeletedDate != null || member.Status != Domain.Enums.MemberStatus.Active)
            await throwBusinessException(TenantsBusinessMessages.MemberNotActive);
    }

    public async Task StaffShouldBeActivated(Staff staff)
    {
        if (staff.DeletedDate != null || !staff.IsActive)
            await throwBusinessException(TenantsBusinessMessages.StaffNotActive);
    }

    public async Task TenantIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Tenant? tenant = await _tenantRepository.GetAsync(
            predicate: t => t.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TenantShouldExistWhenSelected(tenant);
    }

    public async Task throwTenantMemberShouldExist()
    {
        await throwBusinessException(TenantsBusinessMessages.TenantMemberNotExists);
    }
}
