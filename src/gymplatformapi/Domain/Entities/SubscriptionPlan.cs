using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities;

public class SubscriptionPlan : TenantEntity<int>
{
    public string Name { get; set; } = default!;
    public int DurationDays { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = "TRY";
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    public SubscriptionPlan() { }

    public SubscriptionPlan(
        Guid tenantId,
        string name,
        int durationDays,
        decimal price,
        string? description,
        string currency = "TRY",
        bool isActive = true
    )
        : base(tenantId)
    {
        Name = name;
        DurationDays = durationDays;
        Price = price;
        Description = description;
        IsActive = isActive;
        Currency = currency;
    }
}
