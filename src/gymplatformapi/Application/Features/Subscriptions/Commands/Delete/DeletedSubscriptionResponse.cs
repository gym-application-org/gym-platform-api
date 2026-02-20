using Core.Application.Responses;

namespace Application.Features.Subscriptions.Commands.Delete;

public class DeletedSubscriptionResponse : IResponse
{
    public int Id { get; set; }
}
