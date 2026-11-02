using Application.Conditions.Dtos;

namespace Application.Conditions
{
    public interface IConditionService
    {
        Task<IReadOnlyList<ConditionDto>> GetAllAsync(CancellationToken ct = default);
    }
}
