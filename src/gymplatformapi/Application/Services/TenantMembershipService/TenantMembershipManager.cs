using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;

namespace Application.Services.TenantMembershipService;

public class TenantMembershipManager : ITenantMembershipService
{
    private readonly ITenantMembershipRepository _tenantMembershipRepository;

    public TenantMembershipManager(ITenantMembershipRepository tenantMembershipRepository)
    {
        _tenantMembershipRepository = tenantMembershipRepository;
    }

    public async Task<bool> IsUserAllowedInTenantAsync(int userId, Guid tenantId, CancellationToken cancellationToken)
    {
        return await _tenantMembershipRepository.IsUserAllowedInTenantAsync(userId, tenantId, cancellationToken);
    }
}
