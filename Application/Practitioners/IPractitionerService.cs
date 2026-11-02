using Application.Practitioners.Dtos;

namespace Application.Practitioners
{
    public interface IPractitionerService
    {
        Task<IReadOnlyList<PractitionerDto>> GetBySpecialtyAsync(string specialtyCode, CancellationToken ct = default);
    }
}
