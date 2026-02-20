using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DietTemplateMealItemConfiguration : IEntityTypeConfiguration<DietTemplateMealItem>
{
    public void Configure(EntityTypeBuilder<DietTemplateMealItem> builder)
    {
        builder.ToTable("DietTemplateMealItems").HasKey(dtmi => dtmi.Id);

        builder.Property(dtmi => dtmi.Id).HasColumnName("Id").IsRequired();
        builder.Property(dtmi => dtmi.Order).HasColumnName("Order");
        builder.Property(dtmi => dtmi.FoodName).HasColumnName("FoodName");
        builder.Property(dtmi => dtmi.Quantity).HasColumnName("Quantity");
        builder.Property(dtmi => dtmi.Unit).HasColumnName("Unit");
        builder.Property(dtmi => dtmi.Calories).HasColumnName("Calories");
        builder.Property(dtmi => dtmi.ProteinG).HasColumnName("ProteinG");
        builder.Property(dtmi => dtmi.CarbG).HasColumnName("CarbG");
        builder.Property(dtmi => dtmi.FatG).HasColumnName("FatG");
        builder.Property(dtmi => dtmi.Note).HasColumnName("Note");
        builder.Property(dtmi => dtmi.DietTemplateMealId).HasColumnName("DietTemplateMealId");
        builder.Property(dtmi => dtmi.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(dtmi => dtmi.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(dtmi => dtmi.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(dtmi => !dtmi.DeletedDate.HasValue);
    }
}
