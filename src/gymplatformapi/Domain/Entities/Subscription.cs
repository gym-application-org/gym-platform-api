using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Subscription : TenantEntity<int>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SubscriptionStatus Status { get; set; }

    public string PurchasedPlanName { get; set; } = default!;
    public int PurchasedDurationDays { get; set; }
    public decimal PurchasedUnitPrice { get; set; }
    public string Currency { get; set; } = default!;

    public decimal DiscountAmount { get; set; } 
    public decimal TotalPaid { get; set; }   

    public Guid MemberId { get; set; }
    public virtual Member Member { get; set; } = null!;

    public int SubscriptionPlanId { get; set; }
    public virtual SubscriptionPlan SubscriptionPlan { get; set; } = null!;


    public Subscription() { }

    public Subscription(
        Guid tenantId,
        Guid memberId,
        SubscriptionPlan plan,
        DateTime startDate,
        decimal discountAmount = 0m) : base(tenantId)
    {
        MemberId = memberId;
        SubscriptionPlanId = plan.Id;

        StartDate = startDate;
        EndDate = startDate.AddDays(plan.DurationDays);
        Status = SubscriptionStatus.Active;

        PurchasedPlanName = plan.Name;
        PurchasedDurationDays = plan.DurationDays;
        PurchasedUnitPrice = plan.Price;
        Currency = plan.Currency;

        DiscountAmount = discountAmount < 0 ? 0 : discountAmount;
        TotalPaid = Math.Max(0, PurchasedUnitPrice - DiscountAmount);
    }


}
