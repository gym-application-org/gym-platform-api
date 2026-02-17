using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class SupportTicket : TenantEntity<int>
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }

    public DateTime? ClosedAt { get; set; }

    public Guid CreatedByStaffId { get; set; }
    public virtual Staff CreatedByStaff { get; set; } = null!;

    public SupportTicket() { }

    public SupportTicket(Guid tenantId, Guid createdByStaffId, string title, string description, TicketPriority priority)
        : base(tenantId)
    {
        CreatedByStaffId = createdByStaffId;
        Title = title;
        Description = description;
        Priority = priority;
        Status = TicketStatus.Open;
    }
}
