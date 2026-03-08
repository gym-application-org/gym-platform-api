using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Enums;

namespace Domain.Entities;

public class UserActionToken : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid? TenantId { get; set; }

    public string Email { get; set; } = null!;

    public UserActionPurpose Purpose { get; set; }
    public UserActionTargetType? TargetType { get; set; }
    public Guid? TargetEntityId { get; set; }

    public string TokenHash { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }
    public DateTime? RevokedAt { get; set; }

    public Guid? ReplacedByInvitationId { get; set; }

    public Guid? CreatedByUserId { get; set; }
    public string? MetadataJson { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Tenant? Tenant { get; set; }
}
