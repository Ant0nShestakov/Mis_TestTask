using Domain.Entities.Practitioners;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PractitionerConfiguration : IEntityTypeConfiguration<Practitioner>
    {
        public void Configure(EntityTypeBuilder<Practitioner> builder)
        {
            builder.ToTable("Practitioners");

            builder.Property(p => p.GenderType).HasConversion<string>().HasMaxLength(10);

            builder.HasMany(p => p.PractitionerNames)
                .WithOne(n => n.Practitioner)
                .HasForeignKey(n => n.PractitionerId);

            builder.HasMany(p => p.PractitionerIdentifiers)
                .WithOne(i => i.Practitioner)
                .HasForeignKey(i => i.PractitionerId);

            builder.HasMany(p => p.PractitionerQualification)
                .WithOne(q => q.Practitioner)
                .HasForeignKey(q => q.PractitionerId);
        }

    }
}
