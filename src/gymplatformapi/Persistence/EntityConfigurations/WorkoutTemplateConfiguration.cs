using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class WorkoutTemplateConfiguration : IEntityTypeConfiguration<WorkoutTemplate>
{
    public void Configure(EntityTypeBuilder<WorkoutTemplate> builder)
    {
        builder.ToTable("WorkoutTemplates").HasKey(wt => wt.Id);

        builder.Property(wt => wt.Id).HasColumnName("Id").IsRequired();
        builder.Property(wt => wt.Name).HasColumnName("Name");
        builder.Property(wt => wt.Description).HasColumnName("Description");
        builder.Property(wt => wt.Level).HasColumnName("Level");
        builder.Property(wt => wt.IsActive).HasColumnName("IsActive");
        builder.Property(wt => wt.TenantId).HasColumnName("TenantId").IsRequired();
        builder.Property(wt => wt.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(wt => wt.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(wt => wt.DeletedDate).HasColumnName("DeletedDate");
    }
}
