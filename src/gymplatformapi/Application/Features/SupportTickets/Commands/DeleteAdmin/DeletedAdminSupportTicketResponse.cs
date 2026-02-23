using Core.Application.Responses;

namespace Application.Features.SupportTickets.Commands.DeleteAdmin;

public class DeletedAdminSupportTicketResponse : IResponse
{
    public int Id { get; set; }
}
