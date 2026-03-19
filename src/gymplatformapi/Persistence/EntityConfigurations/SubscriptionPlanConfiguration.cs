using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        builder.ToTable("SubscriptionPlans").HasKey(sp => sp.Id);

        builder.Property(sp => sp.Id).HasColumnName("Id").IsRequired();
        builder.Property(sp => sp.Name).HasColumnName("Name");
        builder.Property(sp => sp.DurationDays).HasColumnName("DurationDays");
        builder.Property(sp => sp.Price).HasColumnName("Price");
        builder.Property(sp => sp.Currency).HasColumnName("Currency");
        builder.Property(sp => sp.Description).HasColumnName("Description");
        builder.Property(sp => sp.IsActive).HasColumnName("IsActive");
        builder.Property(sp => sp.TenantId).HasColumnName("TenantId").IsRequired();
        builder.Property(sp => sp.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sp => sp.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sp => sp.DeletedDate).HasColumnName("DeletedDate");
    }
}
