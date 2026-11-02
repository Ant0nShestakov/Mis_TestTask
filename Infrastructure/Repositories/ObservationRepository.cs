using Application.Common.Interfaces;
using Domain.Entities.Observations;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class ObservationRepository : IObservationRepository
    {
        private readonly TestTaskDbContext _db;
        private readonly ILogger<ObservationRepository> _logger;

        public ObservationRepository(TestTaskDbContext db, ILogger<ObservationRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Observation>> GetByEncounterIdAsync(Guid encounterId, CancellationToken ct = default)
        {
            _logger.LogInformation("Fetching observations for encounter {EncounterId}", encounterId);

            return await _db.Observations
                .Where(o => o.EncounterId == encounterId)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Observation>> GetByEncounterIdsAsync(IEnumerable<Guid> encounterIds, CancellationToken ct = default)
        {
            var ids = encounterIds.ToList();

            _logger.LogInformation("Fetching observations for {Count} encounters", ids.Count);

            return await _db.Observations
                .Where(o => o.EncounterId.HasValue && ids.Contains(o.EncounterId.Value))
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}
