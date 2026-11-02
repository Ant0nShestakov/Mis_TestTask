using Application.Practitioners.Dtos;

namespace Application.Practitioners
{
    public interface IPractitionerService
    {
        Task<IReadOnlyList<SpecialtyDto>> GetAllSpecialtiesAsync(CancellationToken ct = default);

        Task<IReadOnlyList<PractitionerDto>> GetBySpecialtyAsync(string specialtyCode, CancellationToken ct = default);

        Task<IReadOnlyList<PractitionerDto>> GetAllAsync(CancellationToken ct = default);

    }
}
