using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class WorkoutTemplateDayConfiguration : IEntityTypeConfiguration<WorkoutTemplateDay>
{
    public void Configure(EntityTypeBuilder<WorkoutTemplateDay> builder)
    {
        builder.ToTable("WorkoutTemplateDays").HasKey(wtd => wtd.Id);

        builder.Property(wtd => wtd.Id).HasColumnName("Id").IsRequired();
        builder.Property(wtd => wtd.DayNumber).HasColumnName("DayNumber");
        builder.Property(wtd => wtd.Title).HasColumnName("Title");
        builder.Property(wtd => wtd.Notes).HasColumnName("Notes");
        builder.Property(wtd => wtd.WorkoutTemplateId).HasColumnName("WorkoutTemplateId");
        builder.Property(wtd => wtd.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(wtd => wtd.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(wtd => wtd.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(wtd => !wtd.DeletedDate.HasValue);
    }
}
