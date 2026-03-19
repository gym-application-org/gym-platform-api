using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EmailOtpConfiguration : IEntityTypeConfiguration<EmailOtp>
{
    public void Configure(EntityTypeBuilder<EmailOtp> builder)
    {
        builder.ToTable("EmailOtps").HasKey(eo => eo.Id);

        builder.Property(eo => eo.Id).HasColumnName("Id").IsRequired();
        builder.Property(eo => eo.Email).HasColumnName("Email");
        builder.Property(eo => eo.CodeHash).HasColumnName("CodeHash");
        builder.Property(eo => eo.Purpose).HasColumnName("Purpose");
        builder.Property(eo => eo.ExpiresAt).HasColumnName("ExpiresAt");
        builder.Property(eo => eo.UsedDate).HasColumnName("UsedDate");
        builder.Property(eo => eo.IsUsed).HasColumnName("IsUsed");
        builder.Property(eo => eo.TryCount).HasColumnName("TryCount");
        builder.Property(eo => eo.TenantId).HasColumnName("TenantId");
        builder.Property(eo => eo.UserId).HasColumnName("UserId");
        builder.Property(eo => eo.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(eo => eo.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(eo => eo.DeletedDate).HasColumnName("DeletedDate");
    }
}
