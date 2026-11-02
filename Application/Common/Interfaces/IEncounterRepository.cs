using Domain.Entities.Encounters;

namespace Application.Common.Interfaces
{
    public interface IEncounterRepository
    {
        Task<IReadOnlyList<Encounter>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    }
}
