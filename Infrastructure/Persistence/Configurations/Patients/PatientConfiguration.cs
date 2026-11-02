using Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        private void AddConstraints(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_Patient_Deceased",
                "\"IsDeceased\" = true OR \"DeceasedDateTime\" IS NULL"
            ));
        }

        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.Property(p => p.GenderType).HasConversion<string>().HasMaxLength(10);

            builder.OwnsOne(p => p.MaritalStatus, mb => 
            {
                mb.Property(m => m.Code).HasColumnName("MaritalStatus_Code");
                mb.Property(m => m.System).HasColumnName("MaritalStatus_System");
                mb.Property(m => m.DisplayName).HasColumnName("MaritalStatus_DisplayName");
            });

            builder.HasMany(p => p.PatientNames)
                .WithOne(n => n.Patient)
                .HasForeignKey(n => n.PatientId);

            builder.HasMany(p => p.PatientIdentifiers)
                .WithOne(i => i.Patient)
                .HasForeignKey(i => i.PatientId);

            builder.HasMany(p => p.PatientTelecoms)
                .WithOne(t => t.Patient)
                .HasForeignKey(t => t.PatientId);

            builder.HasMany(p => p.PatientAddresses)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId);

            AddConstraints(builder);
        }
    }
}
