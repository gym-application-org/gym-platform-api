using Core.Application.Responses;

namespace Application.Features.SupportTickets.Commands.Delete;

public class DeletedSupportTicketResponse : IResponse
{
    public int Id { get; set; }
}
