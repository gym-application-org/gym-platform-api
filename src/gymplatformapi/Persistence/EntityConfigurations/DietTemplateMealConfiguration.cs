using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DietTemplateMealConfiguration : IEntityTypeConfiguration<DietTemplateMeal>
{
    public void Configure(EntityTypeBuilder<DietTemplateMeal> builder)
    {
        builder.ToTable("DietTemplateMeals").HasKey(dtm => dtm.Id);

        builder.Property(dtm => dtm.Id).HasColumnName("Id").IsRequired();
        builder.Property(dtm => dtm.Name).HasColumnName("Name");
        builder.Property(dtm => dtm.Order).HasColumnName("Order");
        builder.Property(dtm => dtm.Notes).HasColumnName("Notes");
        builder.Property(dtm => dtm.DietTemplateDayId).HasColumnName("DietTemplateDayId");
        builder.Property(dtm => dtm.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(dtm => dtm.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(dtm => dtm.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(dtm => !dtm.DeletedDate.HasValue);
    }
}
