using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Entities;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Member : TenantEntity<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public MemberStatus Status { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();
    public virtual ICollection<DietAssignment> DietAssignments { get; set; } = new List<DietAssignment>();
    public virtual ICollection<ProgressEntry> ProgressEntries { get; set; } = new List<ProgressEntry>();
    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public virtual ICollection<WorkoutAssignment> WorkoutAssignments { get; set; } = new List<WorkoutAssignment>();

    public Member() { }

    public Member(Guid tenantId, string firstName, string lastName, string? phone, string? email)
        : base(tenantId)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Status = MemberStatus.Active;
    }
}
