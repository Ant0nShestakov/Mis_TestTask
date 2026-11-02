using Domain.Entities.Encounters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EncounterConfiguration : IEntityTypeConfiguration<Encounter>
    {
        public void Configure(EntityTypeBuilder<Encounter> builder)
        {
            builder.ToTable("Encounters");

            builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);
            builder.Property(e => e.Class).HasConversion<string>().HasMaxLength(20);

            builder.OwnsOne(e => e.EncounterType, tb =>
            {
                tb.Property(t => t.Code).HasColumnName("Type_Code");
                tb.Property(t => t.System).HasColumnName("Type_System");
                tb.Property(t => t.DisplayName).HasColumnName("Type_DisplayName");
            });

            builder.HasOne(e => e.Patient)
                .WithMany()
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Practitioner)
                .WithMany()
                .HasForeignKey(e => e.PractitionerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
