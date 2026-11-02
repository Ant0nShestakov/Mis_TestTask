using Application.Patients.Dtos;

namespace Application.Patients
{
    public interface IPatientService
    {
        Task<IReadOnlyList<PatientShortDto>> GetAllAsync(CancellationToken ct = default);

        Task<PatientDTO> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}
