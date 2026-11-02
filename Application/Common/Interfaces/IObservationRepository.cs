namespace Application.Common.Interfaces
{
    public interface IObservationRepository
    {
        Task<IReadOnlyList<Domain.Entities.Observations.Observation>> GetByEncounterIdAsync(Guid encounterId, CancellationToken ct = default);

        Task<IReadOnlyList<Domain.Entities.Observations.Observation>> GetByEncounterIdsAsync(IEnumerable<Guid> encounterIds, CancellationToken ct = default);
    }
}
