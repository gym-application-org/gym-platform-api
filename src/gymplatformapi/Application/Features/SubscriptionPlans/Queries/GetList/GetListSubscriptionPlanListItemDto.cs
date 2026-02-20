using Core.Application.Dtos;

namespace Application.Features.SubscriptionPlans.Queries.GetList;

public class GetListSubscriptionPlanListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DurationDays { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
