using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("Exercises").HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
        builder.Property(e => e.Name).HasColumnName("Name");
        builder.Property(e => e.Description).HasColumnName("Description");
        builder.Property(e => e.MuscleGroup).HasColumnName("MuscleGroup");
        builder.Property(e => e.Equipment).HasColumnName("Equipment");
        builder.Property(e => e.DifficultyLevel).HasColumnName("DifficultyLevel");
        builder.Property(e => e.VideoUrl).HasColumnName("VideoUrl");
        builder.Property(e => e.ThumbnailUrl).HasColumnName("ThumbnailUrl");
        builder.Property(e => e.IsActive).HasColumnName("IsActive");
        builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(e => e.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(e => e.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(e => !e.DeletedDate.HasValue);
    }
}
