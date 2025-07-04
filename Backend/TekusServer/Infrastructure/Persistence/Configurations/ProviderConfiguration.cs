using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public sealed class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nit)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne(p => p.Email, emailBuilder =>
            {
                emailBuilder.Property(e => e.Value)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(100);
            });

            builder.HasMany(p => p.CustomFields)
                .WithOne()
                .HasForeignKey("ProviderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Services)
                .WithOne()
                .HasForeignKey("ProviderId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(p => p.CustomFields).AutoInclude();

            builder.Navigation(p => p.Services).AutoInclude();

            /*builder.Entity<Service>()
                .Property(s => s.Id).ValueGeneratedNever();*/

        }
    }
}
