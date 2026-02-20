using Core.Application.Responses;

namespace Application.Features.SubscriptionPlans.Commands.Create;

public class CreatedSubscriptionPlanResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DurationDays { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
