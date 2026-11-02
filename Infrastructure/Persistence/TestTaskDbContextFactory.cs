using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence
{
    public class TestTaskDbContextFactory : IDesignTimeDbContextFactory<TestTaskDbContext>
    {
        public TestTaskDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestTaskDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=dummy;Username=dummy;Password=dummy");

            return new TestTaskDbContext(optionsBuilder.Options);
        }
    }
}
