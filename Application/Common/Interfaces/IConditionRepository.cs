
using Domain.Entities.Conditions;

namespace Application.Common.Interfaces
{
    public interface IConditionRepository
    {
        Task<IReadOnlyList<Condition>> GetAllAsync(CancellationToken ct = default);

        Task<IReadOnlyList<Condition>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    }
}
