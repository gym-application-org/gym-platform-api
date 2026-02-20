using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class WorkoutAssignmentConfiguration : IEntityTypeConfiguration<WorkoutAssignment>
{
    public void Configure(EntityTypeBuilder<WorkoutAssignment> builder)
    {
        builder.ToTable("WorkoutAssignments").HasKey(wa => wa.Id);

        builder.Property(wa => wa.Id).HasColumnName("Id").IsRequired();
        builder.Property(wa => wa.StartDate).HasColumnName("StartDate");
        builder.Property(wa => wa.EndDate).HasColumnName("EndDate");
        builder.Property(wa => wa.Status).HasColumnName("Status");
        builder.Property(wa => wa.MemberId).HasColumnName("MemberId");
        builder.Property(wa => wa.WorkoutTemplateId).HasColumnName("WorkoutTemplateId");
        builder.Property(wa => wa.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(wa => wa.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(wa => wa.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(wa => !wa.DeletedDate.HasValue);
    }
}
