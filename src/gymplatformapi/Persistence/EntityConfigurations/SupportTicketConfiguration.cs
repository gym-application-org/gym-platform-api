using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicket>
{
    public void Configure(EntityTypeBuilder<SupportTicket> builder)
    {
        builder.ToTable("SupportTickets").HasKey(st => st.Id);

        builder.Property(st => st.Id).HasColumnName("Id").IsRequired();
        builder.Property(st => st.Title).HasColumnName("Title");
        builder.Property(st => st.Description).HasColumnName("Description");
        builder.Property(st => st.Status).HasColumnName("Status");
        builder.Property(st => st.Priority).HasColumnName("Priority");
        builder.Property(st => st.ClosedAt).HasColumnName("ClosedAt");
        builder.Property(st => st.CreatedByStaffId).HasColumnName("CreatedByStaffId");
        builder.Property(st => st.TenantId).HasColumnName("TenantId").IsRequired();
        builder.Property(st => st.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(st => st.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(st => st.DeletedDate).HasColumnName("DeletedDate");
    }
}
