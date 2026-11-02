using Application.Common.Interfaces;
using Domain.Entities.Conditions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Infrastructure.Repositories
{
    public class ConditionRepository : IConditionRepository
    {
        private readonly TestTaskDbContext _db;
        private readonly ILogger<ConditionRepository> _logger;

        public ConditionRepository(TestTaskDbContext db, ILogger<ConditionRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Condition>> GetAllAsync(CancellationToken ct = default)
        {
            _logger.LogInformation("Fetching all conditions (diseases).");

            return await _db.Conditions
                .Include(c => c.Patient)
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}
