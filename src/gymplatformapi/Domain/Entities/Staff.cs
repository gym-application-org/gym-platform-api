using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Staff : TenantEntity<Guid>
{
    public string Name { get; set; } = default!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public StaffRole Role { get; set; }
    public bool IsActive { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public Staff() { }

    public Staff(Guid tenantId, string name, StaffRole role, string? phone, string? email, bool isActive = true)
        : base(tenantId)
    {
        Name = name;
        Role = role;
        Phone = phone;
        Email = email;
        IsActive = isActive;
    }
}
