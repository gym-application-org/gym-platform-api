using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members").HasKey(m => m.Id);

        builder.Property(m => m.Id).HasColumnName("Id").IsRequired();
        builder.Property(m => m.FirstName).HasColumnName("FirstName");
        builder.Property(m => m.LastName).HasColumnName("LastName");
        builder.Property(m => m.Phone).HasColumnName("Phone");
        builder.Property(m => m.Email).HasColumnName("Email");
        builder.Property(m => m.Status).HasColumnName("Status");
        builder.Property(m => m.UserId).HasColumnName("UserId");
        builder.Property(m => m.TenantId).HasColumnName("TenantId").IsRequired();
        builder.Property(m => m.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(m => m.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(m => m.DeletedDate).HasColumnName("DeletedDate");
    }
}
