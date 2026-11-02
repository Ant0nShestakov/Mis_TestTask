using Domain.Entities.Observations;

namespace Application.Common.Interfaces
{
    public interface IObservationRepository
    {
        Task<IReadOnlyList<Observation>> GetByEncounterIdAsync(Guid encounterId, CancellationToken ct = default);
    }
}
