using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions").HasKey(s => s.Id);

        builder.Property(s => s.Id).HasColumnName("Id").IsRequired();
        builder.Property(s => s.StartDate).HasColumnName("StartDate");
        builder.Property(s => s.EndDate).HasColumnName("EndDate");
        builder.Property(s => s.Status).HasColumnName("Status");
        builder.Property(s => s.PurchasedPlanName).HasColumnName("PurchasedPlanName");
        builder.Property(s => s.PurchasedDurationDays).HasColumnName("PurchasedDurationDays");
        builder.Property(s => s.PurchasedUnitPrice).HasColumnName("PurchasedUnitPrice");
        builder.Property(s => s.Currency).HasColumnName("Currency");
        builder.Property(s => s.DiscountAmount).HasColumnName("DiscountAmount");
        builder.Property(s => s.TotalPaid).HasColumnName("TotalPaid");
        builder.Property(s => s.MemberId).HasColumnName("MemberId");
        builder.Property(s => s.SubscriptionPlanId).HasColumnName("SubscriptionPlanId");
        builder.Property(s => s.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(s => s.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(s => s.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(s => !s.DeletedDate.HasValue);
    }
}
