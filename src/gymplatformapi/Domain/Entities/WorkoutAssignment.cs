using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class WorkoutAssignment : TenantEntity<int>
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }

    public Guid MemberId { get; set; }
    public virtual Member Member { get; set; } = null!;

    public int WorkoutTemplateId { get; set; }
    public virtual WorkoutTemplate WorkoutTemplate { get; set; } = null!;

    public WorkoutAssignment() { }

    public WorkoutAssignment(Guid tenantId, Guid memberId, int workoutTemplateId, DateTime startDate, DateTime? endDate)
        : base(tenantId)
    {
        MemberId = memberId;
        WorkoutTemplateId = workoutTemplateId;
        StartDate = startDate;
        EndDate = endDate;
        Status = AssignmentStatus.Active;
    }
}
