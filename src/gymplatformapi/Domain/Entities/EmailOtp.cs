using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Enums;

namespace Domain.Entities;

public class EmailOtp : Entity<Guid>
{
    public string Email { get; set; }
    public string CodeHash { get; set; }
    public OtpPurpose Purpose { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedDate { get; set; }
    public bool IsUsed { get; set; }
    public int TryCount { get; set; }
    public Guid? TenantId { get; set; }
    public Guid? UserId { get; set; }

    public virtual Tenant? Tenant { get; set; }
    public virtual User? User { get; set; }

    public EmailOtp()
    {
        Email = string.Empty;
        CodeHash = string.Empty;
    }

    public EmailOtp(
        Guid id,
        string email,
        string codeHash,
        OtpPurpose purpose,
        DateTime expiresAt,
        DateTime? usedDate,
        bool isUsed,
        int tryCount,
        Guid? tenantId,
        Guid? userId
    )
        : base(id)
    {
        Email = email;
        CodeHash = codeHash;
        Purpose = purpose;
        ExpiresAt = expiresAt;
        UsedDate = usedDate;
        IsUsed = isUsed;
        TryCount = tryCount;
        TenantId = tenantId;
        UserId = userId;
    }
}
