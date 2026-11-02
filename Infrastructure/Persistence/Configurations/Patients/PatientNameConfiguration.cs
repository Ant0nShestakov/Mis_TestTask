using Domain.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations
{
    public class PatientNameConfiguration : IEntityTypeConfiguration<PatientName>
    {
        public void Configure(EntityTypeBuilder<PatientName> builder)
        {
            builder.ToTable("PatientNames");

            builder.Property(n => n.Family).HasMaxLength(100);
            builder.Property(n => n.Name).HasMaxLength(100);
            builder.Property(n => n.Patronymic).HasMaxLength(100);
        }
    }
}

