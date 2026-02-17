using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class DietAssignment : TenantEntity<int>
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public AssignmentStatus Status { get; set; }

    public Guid MemberId { get; set; }
    public Member Member { get; set; } = null!;

    public int DietTemplateId { get; set; }
    public virtual DietTemplate DietTemplate { get; set; } = null!;

    public DietAssignment() { }

    public DietAssignment(Guid tenantId, Guid memberId, int dietTemplateId, DateTime startDate, DateTime? endDate)
        : base(tenantId)
    {
        MemberId = memberId;
        DietTemplateId = dietTemplateId;
        StartDate = startDate;
        EndDate = endDate;
        Status = AssignmentStatus.Active;
    }
}
