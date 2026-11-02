using Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PatientAddressConfiguration : IEntityTypeConfiguration<PatientAddresses>
    {
        public void Configure(EntityTypeBuilder<PatientAddresses> builder)
        {
            builder.ToTable("PatientAddresses");

            builder.Property(a => a.AddressType)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(a => a.FullAddress)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
