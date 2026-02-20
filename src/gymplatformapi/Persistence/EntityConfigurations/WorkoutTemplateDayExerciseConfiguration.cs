using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class WorkoutTemplateDayExerciseConfiguration : IEntityTypeConfiguration<WorkoutTemplateDayExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutTemplateDayExercise> builder)
    {
        builder.ToTable("WorkoutTemplateDayExercises").HasKey(wtde => wtde.Id);

        builder.Property(wtde => wtde.Id).HasColumnName("Id").IsRequired();
        builder.Property(wtde => wtde.Order).HasColumnName("Order");
        builder.Property(wtde => wtde.Sets).HasColumnName("Sets");
        builder.Property(wtde => wtde.Reps).HasColumnName("Reps");
        builder.Property(wtde => wtde.WeightKg).HasColumnName("WeightKg");
        builder.Property(wtde => wtde.RestSeconds).HasColumnName("RestSeconds");
        builder.Property(wtde => wtde.Tempo).HasColumnName("Tempo");
        builder.Property(wtde => wtde.Note).HasColumnName("Note");
        builder.Property(wtde => wtde.WorkoutTemplateDayId).HasColumnName("WorkoutTemplateDayId");
        builder.Property(wtde => wtde.ExerciseId).HasColumnName("ExerciseId");
        builder.Property(wtde => wtde.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(wtde => wtde.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(wtde => wtde.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(wtde => !wtde.DeletedDate.HasValue);
    }
}
