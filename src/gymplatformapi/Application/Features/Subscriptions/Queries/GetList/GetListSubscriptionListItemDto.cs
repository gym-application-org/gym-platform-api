using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.Subscriptions.Queries.GetList;

public class GetListSubscriptionListItemDto : IDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SubscriptionStatus Status { get; set; }
    public string PurchasedPlanName { get; set; }
    public int PurchasedDurationDays { get; set; }
    public decimal PurchasedUnitPrice { get; set; }
    public string Currency { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalPaid { get; set; }
    public Guid MemberId { get; set; }
    public int SubscriptionPlanId { get; set; }
}
