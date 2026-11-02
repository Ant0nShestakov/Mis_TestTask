using Application.Common.Interfaces;
using Application.Practitioners.Dtos;

namespace Application.Practitioners;

public class PractitionerService : IPractitionerService
{
    private readonly IPractitionerRepository _repository;

    public PractitionerService(IPractitionerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PractitionerDto>> GetBySpecialtyAsync(string specialtyCode, CancellationToken ct = default)
    {
        var practitioners = await _repository.GetBySpecialtyAsync(specialtyCode, ct);

        return practitioners.Select(p => new PractitionerDto
        {
            Id = p.Id,
            FullName = p.PractitionerNames?.FirstOrDefault() != null
                ? $"{p.PractitionerNames.First().Family} {p.PractitionerNames.First().Name}".Trim()
                : "Имя не указано",
            Specialties = p.PractitionerQualification
                .Select(q => q.Code.DisplayName ?? "Неизвестно").ToList()
        }).ToList();
    }
}