
using Domain.Entities.Conditions;

namespace Application.Common.Interfaces
{
    public interface IConditionRepository
    {
        Task<IReadOnlyList<Condition>> GetAllAsync(CancellationToken ct = default);
    }
}
