using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.TenantMembershipService;

public interface ITenantMembershipService
{
    Task<bool> IsUserAllowedInTenantAsync(int userId, Guid tenantId, CancellationToken cancellationToken);
}
