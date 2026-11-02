using Domain.Entities.Observations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations
{
    public class ObservationConfiguration : IEntityTypeConfiguration<Observation>
    {
        public void Configure(EntityTypeBuilder<Observation> builder)
        {
            builder.ToTable("Observations");

            builder.Property(o => o.ObservationStatus).HasConversion<string>().HasMaxLength(30);

            builder.OwnsOne(o => o.Code, cb =>
            {
                cb.Property(x => x.Code).HasColumnName("Code_Code").IsRequired();
                cb.Property(x => x.System).HasColumnName("Code_System");
                cb.Property(x => x.DisplayName).HasColumnName("Code_DisplayName");
                cb.HasIndex(x => x.Code);
            });

            builder.Property(o => o.ValueQuantity).HasPrecision(18, 4);

            builder.HasOne(o => o.Patient)
                .WithMany()
                .HasForeignKey(o => o.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Encounter)
                .WithMany()
                .HasForeignKey(o => o.EncounterId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
