using Core.Application.Responses;
using Domain.Enums;
using Domain.Enums;

namespace Application.Features.SupportTickets.Commands.Create;

public class CreatedSupportTicketResponse : IResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public DateTime? ClosedAt { get; set; }
    public Guid CreatedByStaffId { get; set; }
}
