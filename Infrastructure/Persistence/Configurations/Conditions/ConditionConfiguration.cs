using Domain.Entities.Conditions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ConditionConfiguration : IEntityTypeConfiguration<Condition>
    {
        public void Configure(EntityTypeBuilder<Condition> builder)
        {
            builder.ToTable("Conditions");

            builder.Property(c => c.ClinicalStatus).HasConversion<string>().HasMaxLength(30);
            builder.Property(c => c.VerificationStatus).HasConversion<string>().HasMaxLength(30);
            builder.Property(c => c.Severity).HasConversion<string>().HasMaxLength(30);

            builder.OwnsOne(c => c.Code, cb =>
            {
                cb.Property(x => x.Code).HasColumnName("Code_Code").IsRequired();
                cb.Property(x => x.System).HasColumnName("Code_System");
                cb.Property(x => x.DisplayName).HasColumnName("Code_DisplayName");
                cb.HasIndex(x => x.Code);
            });

            builder.HasOne(c => c.Patient)
                .WithMany()
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Encounter)
                .WithMany()
                .HasForeignKey(c => c.EncounterId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.Recorder)
                .WithMany()
                .HasForeignKey(c => c.RecorderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
