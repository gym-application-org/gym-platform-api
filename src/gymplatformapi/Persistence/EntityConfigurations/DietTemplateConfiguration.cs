using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DietTemplateConfiguration : IEntityTypeConfiguration<DietTemplate>
{
    public void Configure(EntityTypeBuilder<DietTemplate> builder)
    {
        builder.ToTable("DietTemplates").HasKey(dt => dt.Id);

        builder.Property(dt => dt.Id).HasColumnName("Id").IsRequired();
        builder.Property(dt => dt.Name).HasColumnName("Name");
        builder.Property(dt => dt.Description).HasColumnName("Description");
        builder.Property(dt => dt.CaloriesTarget).HasColumnName("CaloriesTarget");
        builder.Property(dt => dt.ProteinGramsTarget).HasColumnName("ProteinGramsTarget");
        builder.Property(dt => dt.CarbGramsTarget).HasColumnName("CarbGramsTarget");
        builder.Property(dt => dt.FatGramsTarget).HasColumnName("FatGramsTarget");
        builder.Property(dt => dt.IsActive).HasColumnName("IsActive");
        builder.Property(dt => dt.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(dt => dt.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(dt => dt.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(dt => !dt.DeletedDate.HasValue);
    }
}
