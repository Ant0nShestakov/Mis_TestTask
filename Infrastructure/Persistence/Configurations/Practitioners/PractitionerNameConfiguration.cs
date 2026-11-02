using Domain.Entities.Practitioners;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PractitionerNameConfiguration : IEntityTypeConfiguration<PractitionerName>
    {
        public void Configure(EntityTypeBuilder<PractitionerName> builder)
        {
            builder.ToTable("PractitionerNames");

            builder.Property(n => n.Family).HasMaxLength(100);
            builder.Property(n => n.Name).HasMaxLength(100);
            builder.Property(n => n.Patronymic).HasMaxLength(100);
        }
    }
}
