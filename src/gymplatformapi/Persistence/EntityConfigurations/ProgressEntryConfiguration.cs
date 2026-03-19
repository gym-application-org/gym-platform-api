using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProgressEntryConfiguration : IEntityTypeConfiguration<ProgressEntry>
{
    public void Configure(EntityTypeBuilder<ProgressEntry> builder)
    {
        builder.ToTable("ProgressEntries").HasKey(pe => pe.Id);

        builder.Property(pe => pe.Id).HasColumnName("Id").IsRequired();
        builder.Property(pe => pe.Date).HasColumnName("Date");
        builder.Property(pe => pe.WeightKg).HasColumnName("WeightKg");
        builder.Property(pe => pe.BodyFatPercent).HasColumnName("BodyFatPercent");
        builder.Property(pe => pe.MuscleMassKg).HasColumnName("MuscleMassKg");
        builder.Property(pe => pe.ChestCm).HasColumnName("ChestCm");
        builder.Property(pe => pe.WaistCm).HasColumnName("WaistCm");
        builder.Property(pe => pe.HipCm).HasColumnName("HipCm");
        builder.Property(pe => pe.ArmCm).HasColumnName("ArmCm");
        builder.Property(pe => pe.LegCm).HasColumnName("LegCm");
        builder.Property(pe => pe.Note).HasColumnName("Note");
        builder.Property(pe => pe.MemberId).HasColumnName("MemberId");
        builder.Property(pe => pe.TenantId).HasColumnName("TenantId").IsRequired();
        builder.Property(pe => pe.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pe => pe.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pe => pe.DeletedDate).HasColumnName("DeletedDate");
    }
}
