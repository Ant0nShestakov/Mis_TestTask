using Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PatientTelecomConfiguration : IEntityTypeConfiguration<PatientTelecom>
    {
        public void Configure(EntityTypeBuilder<PatientTelecom> builder)
        {
            builder.ToTable("PatientTelecoms");

            builder.Property(t => t.TelecomType)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(t => t.Value)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
