using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TenantMembershipRepository : ITenantMembershipRepository
{
    private readonly BaseDbContext _context;

    public TenantMembershipRepository(BaseDbContext baseDbContext)
    {
        _context = baseDbContext;
    }

    public async Task<bool> IsUserAllowedInTenantAsync(int userId, Guid tenantId, CancellationToken cancellationToken)
    {
        bool isStaff = await _context
            .Set<Domain.Entities.Staff>()
            .AnyAsync(x => x.UserId == userId && x.TenantId == tenantId && x.IsActive, cancellationToken);

        bool isMember = await _context
            .Set<Domain.Entities.Member>()
            .AnyAsync(x => x.UserId == userId && x.TenantId == tenantId && x.Status == MemberStatus.Active, cancellationToken);

        return isStaff || isMember;
    }
}
