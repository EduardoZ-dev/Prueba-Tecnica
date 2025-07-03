using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public sealed class CustomFieldConfiguration : IEntityTypeConfiguration<CustomField>
    {
        public void Configure(EntityTypeBuilder<CustomField> builder)
        {
            builder.HasKey(cf => cf.Id);

            builder.Property(cf => cf.Key)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cf => cf.Value)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(cf => cf.CreatedAt)
                .IsRequired();

            builder.Property(cf => cf.UpdatedAt);
        }
    }
}
