using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class AttendanceLog : TenantEntity<int>
{
    public AttendanceResult Result { get; set; }
    public string? DenyReason { get; set; }

    public Guid MemberId { get; set; }
    public virtual Member Member { get; set; } = null!;

    public int GateId { get; set; }
    public virtual Gate Gate { get; set; } = null!;

    public AttendanceLog() { }

    public AttendanceLog(Guid tenantId, Guid memberId, int gateId, AttendanceResult result, string? denyReason, DateTime timestampUtc)
        : base(tenantId)
    {
        MemberId = memberId;
        GateId = gateId;
        Result = result;
        DenyReason = denyReason;
    }
}
