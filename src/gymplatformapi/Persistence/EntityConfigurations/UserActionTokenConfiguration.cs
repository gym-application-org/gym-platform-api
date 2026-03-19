using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserActionTokenConfiguration : IEntityTypeConfiguration<UserActionToken>
{
    public void Configure(EntityTypeBuilder<UserActionToken> builder)
    {
        builder.ToTable("UserActionTokens").HasKey(uat => uat.Id);

        builder.Property(uat => uat.Id).HasColumnName("Id").IsRequired();
        builder.Property(uat => uat.UserId).HasColumnName("UserId");
        builder.Property(uat => uat.TenantId).HasColumnName("TenantId");
        builder.Property(uat => uat.Email).HasColumnName("Email");
        builder.Property(uat => uat.Purpose).HasColumnName("Purpose");
        builder.Property(uat => uat.TargetType).HasColumnName("TargetType");
        builder.Property(uat => uat.TargetEntityId).HasColumnName("TargetEntityId");
        builder.Property(uat => uat.TokenHash).HasColumnName("TokenHash");
        builder.Property(uat => uat.ExpiresAt).HasColumnName("ExpiresAt");
        builder.Property(uat => uat.UsedAt).HasColumnName("UsedAt");
        builder.Property(uat => uat.RevokedAt).HasColumnName("RevokedAt");
        builder.Property(uat => uat.ReplacedByInvitationId).HasColumnName("ReplacedByInvitationId");
        builder.Property(uat => uat.CreatedByUserId).HasColumnName("CreatedByUserId");
        builder.Property(uat => uat.MetadataJson).HasColumnName("MetadataJson");
        builder.Property(uat => uat.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(uat => uat.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(uat => uat.DeletedDate).HasColumnName("DeletedDate");
    }
}
