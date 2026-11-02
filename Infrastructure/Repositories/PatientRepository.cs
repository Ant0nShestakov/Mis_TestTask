using Application.Common.Interfaces;
using Domain.Entities.Patients;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;


namespace Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly TestTaskDbContext _db;
        private readonly ILogger<PatientRepository> _logger;

        public PatientRepository(TestTaskDbContext db, ILogger<PatientRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Patient>> GetAllAsync(CancellationToken ct = default)
        {
            _logger.LogInformation("Fetching all patients from the database.");

            return await _db.Patients
                .Include(p => p.PatientNames)
                .Include(p => p.PatientIdentifiers)
                    .ThenInclude(i => i.IdentifierType)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            _logger.LogInformation("Fetching patient with ID: {PatientId}", id);

            var patient = await _db.Patients
                .Include(p => p.PatientNames)
                .Include(p => p.PatientIdentifiers)
                    .ThenInclude(i => i.IdentifierType)
                .Include(p => p.PatientTelecoms)
                .Include(p => p.PatientAddresses)
                .FirstOrDefaultAsync(p => p.Id == id, ct);

            if (patient == null)
            {
                _logger.LogWarning("Patient with ID {PatientId} was not found.", id);
            }

            return patient;
        }
    }
}
