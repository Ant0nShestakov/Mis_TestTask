using Domain.Entities.Conditions;
using Domain.Entities.Encounters;
using Domain.Entities.Observations;
using Domain.Entities.Patients;
using Domain.Entities.Practitioners;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class TestTaskDbContext : DbContext
    {
        public DbSet<Patient> Patients => Set<Patient>();

        public DbSet<Practitioner> Practitioners => Set<Practitioner>();

        public DbSet<Encounter> Encounters => Set<Encounter>();

        public DbSet<Condition> Conditions => Set<Condition>();

        public DbSet<Observation> Observations => Set<Observation>();


        public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestTaskDbContext).Assembly);
        }
    }
}
