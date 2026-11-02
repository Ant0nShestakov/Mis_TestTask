using Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PatientIdentifierConfiguration : IEntityTypeConfiguration<PatientIdentifiers>
    {
        public void Configure(EntityTypeBuilder<PatientIdentifiers> builder)
        {
            builder.ToTable("PatientIdentifiers");

            builder.OwnsOne(i => i.IdentifierType, ib =>
            {
                ib.Property(x => x.Code).HasColumnName("IdentifierType_Code").IsRequired();
                ib.Property(x => x.System).HasColumnName("IdentifierType_System");
                ib.Property(x => x.DisplayName).HasColumnName("IdentifierType_DisplayName");
            });

            builder.Property(i => i.Value)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(i => i.Value);
        }
    }
}
