using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DietTemplateDayConfiguration : IEntityTypeConfiguration<DietTemplateDay>
{
    public void Configure(EntityTypeBuilder<DietTemplateDay> builder)
    {
        builder.ToTable("DietTemplateDays").HasKey(dtd => dtd.Id);

        builder.Property(dtd => dtd.Id).HasColumnName("Id").IsRequired();
        builder.Property(dtd => dtd.DayNumber).HasColumnName("DayNumber");
        builder.Property(dtd => dtd.Title).HasColumnName("Title");
        builder.Property(dtd => dtd.Notes).HasColumnName("Notes");
        builder.Property(dtd => dtd.DietTemplateId).HasColumnName("DietTemplateId");
        builder.Property(dtd => dtd.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(dtd => dtd.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(dtd => dtd.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(dtd => !dtd.DeletedDate.HasValue);
    }
}
