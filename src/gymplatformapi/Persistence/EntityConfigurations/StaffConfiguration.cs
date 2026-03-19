using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.ToTable("Staffs").HasKey(s => s.Id);

        builder.Property(s => s.Id).HasColumnName("Id").IsRequired();
        builder.Property(s => s.Name).HasColumnName("Name");
        builder.Property(s => s.Phone).HasColumnName("Phone");
        builder.Property(s => s.Email).HasColumnName("Email");
        builder.Property(s => s.Role).HasColumnName("Role");
        builder.Property(s => s.IsActive).HasColumnName("IsActive");
        builder.Property(s => s.UserId).HasColumnName("UserId");
        builder.Property(s => s.TenantId).HasColumnName("TenantId").IsRequired();
        builder.Property(s => s.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(s => s.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(s => s.DeletedDate).HasColumnName("DeletedDate");
    }
}
