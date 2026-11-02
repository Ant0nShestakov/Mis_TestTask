using Application.Common.Interfaces;
using Domain.Entities.Practitioners;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Infrastructure.Repositories
{
    public class PractitionerRepository : IPractitionerRepository
    {
        private readonly TestTaskDbContext _db;
        private readonly ILogger<PractitionerRepository> _logger;

        public PractitionerRepository(TestTaskDbContext db, ILogger<PractitionerRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Practitioner>> GetBySpecialtyAsync(string specialtyCode, CancellationToken ct = default)
        {
            _logger.LogInformation("Fetching practitioners by specialty code: {SpecialtyCode}", specialtyCode);

            return await _db.Practitioners
                .Include(p => p.PractitionerNames)
                .Include(p => p.PractitionerIdentifiers)
                    .ThenInclude(i => i.IdentifierType)
                .Include(p => p.PractitionerQualification)
                .Where(p => p.PractitionerQualification.Any(q => q.Code.Code == specialtyCode))
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}
