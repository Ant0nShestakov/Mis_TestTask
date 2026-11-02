using Application.Common.Interfaces;
using Domain.Entities.Encounters;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Infrastructure.Repositories
{
    public class EncounterRepository : IEncounterRepository
    {
        private readonly TestTaskDbContext _db;
        private readonly ILogger<EncounterRepository> _logger;

        public EncounterRepository(TestTaskDbContext db, ILogger<EncounterRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Encounter>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default)
        {
            _logger.LogInformation("Fetching encounters for patient {PatientId}", patientId);

            return await _db.Encounters
                .Include(e => e.Practitioner)
                .Include(e => e.Practitioner.PractitionerNames)
                .Where(e => e.PatientId == patientId)
                .OrderByDescending(e => e.PeriodStart) 
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}
