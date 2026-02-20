using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AttendanceLogConfiguration : IEntityTypeConfiguration<AttendanceLog>
{
    public void Configure(EntityTypeBuilder<AttendanceLog> builder)
    {
        builder.ToTable("AttendanceLogs").HasKey(al => al.Id);

        builder.Property(al => al.Id).HasColumnName("Id").IsRequired();
        builder.Property(al => al.Result).HasColumnName("Result");
        builder.Property(al => al.DenyReason).HasColumnName("DenyReason");
        builder.Property(al => al.MemberId).HasColumnName("MemberId");
        builder.Property(al => al.GateId).HasColumnName("GateId");
        builder.Property(al => al.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(al => al.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(al => al.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(al => !al.DeletedDate.HasValue);
    }
}
