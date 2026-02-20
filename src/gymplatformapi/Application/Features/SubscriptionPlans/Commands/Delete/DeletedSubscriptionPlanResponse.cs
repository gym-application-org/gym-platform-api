using Core.Application.Responses;

namespace Application.Features.SubscriptionPlans.Commands.Delete;

public class DeletedSubscriptionPlanResponse : IResponse
{
    public int Id { get; set; }
}
