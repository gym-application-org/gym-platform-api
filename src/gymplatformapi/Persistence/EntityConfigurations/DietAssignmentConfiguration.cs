using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DietAssignmentConfiguration : IEntityTypeConfiguration<DietAssignment>
{
    public void Configure(EntityTypeBuilder<DietAssignment> builder)
    {
        builder.ToTable("DietAssignments").HasKey(da => da.Id);

        builder.Property(da => da.Id).HasColumnName("Id").IsRequired();
        builder.Property(da => da.StartDate).HasColumnName("StartDate");
        builder.Property(da => da.EndDate).HasColumnName("EndDate");
        builder.Property(da => da.Status).HasColumnName("Status");
        builder.Property(da => da.MemberId).HasColumnName("MemberId");
        builder.Property(da => da.Member).HasColumnName("Member");
        builder.Property(da => da.DietTemplateId).HasColumnName("DietTemplateId");
        builder.Property(da => da.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(da => da.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(da => da.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(da => !da.DeletedDate.HasValue);
    }
}
