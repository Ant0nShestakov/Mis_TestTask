using Domain.Entities.Practitioners;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PractitionerQualificationConfiguration : IEntityTypeConfiguration<PractitionerQualification>
    {
        public void Configure(EntityTypeBuilder<PractitionerQualification> builder)
        {
            builder.ToTable("PractitionerQualifications");

            builder.Property(q => q.IdentifierValue)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(q => q.Issuer)
                .HasMaxLength(200)
                .IsRequired();

            builder.OwnsOne(q => q.Code, cb =>
            {
                cb.Property(x => x.Code).HasColumnName("Code_Code").IsRequired();
                cb.Property(x => x.System).HasColumnName("Code_System");
                cb.Property(x => x.DisplayName).HasColumnName("Code_DisplayName");
            });
        }
    }
}
