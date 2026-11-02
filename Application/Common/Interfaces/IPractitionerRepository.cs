using Domain.Entities.Practitioners;


namespace Application.Common.Interfaces
{
    public interface IPractitionerRepository
    {
        Task<IReadOnlyList<Practitioner>> GetBySpecialtyAsync(string specialtyCode, CancellationToken ct = default);
    }
}
