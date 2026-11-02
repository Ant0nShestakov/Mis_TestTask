using Application.Common.Dtos;
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

    private static List<PractitionerDto> MapToDto(IReadOnlyList<Domain.Entities.Practitioners.Practitioner> practitioners)
    {
        return practitioners.Select(p =>
        {
            var primaryName = p.PractitionerNames.FirstOrDefault();

            return new PractitionerDto
            {
                Id = p.Id,
                IsActive = p.IsActive,
                Gender = p.GenderType.ToString(),
                BirthDate = p.BirthDate,

                FullName = new HumanNameDto
                {
                    Family = primaryName?.Family ?? "Не указана",
                    Name = primaryName?.Name ?? "Не указано",
                    Patronymic = primaryName?.Patronymic
                },

                Identifiers = p.PractitionerIdentifiers.Select(i => new PractitionerIdentifierDto
                {
                    System = i.IdentifierType?.DisplayName ?? "Неизвестно",
                    Value = i.Value
                }).ToList(),

                Qualifications = p.PractitionerQualification.Select(q => new QualificationDto
                {
                    Code = q.Code.Code,
                    DisplayName = q.Code.DisplayName ?? "Без названия",
                    Issuer = q.Issuer,
                    PeriodStart = q.PeriodStart,
                    PeriodEnd = q.PeriodEnd
                }).ToList()
            };
        }).ToList();
    }

    public async Task<IReadOnlyList<PractitionerDto>> GetAllAsync(CancellationToken ct = default)
    {
        var practitioners = await _repository.GetAllAsync(ct);
        return MapToDto(practitioners);
    }

    public async Task<IReadOnlyList<SpecialtyDto>> GetAllSpecialtiesAsync(CancellationToken ct = default)
    {
        var practitioners = await _repository.GetAllAsync(ct);

        var allQualifications = practitioners
            .SelectMany(p => p.PractitionerQualification)
            .Where(q => q.Code != null && !string.IsNullOrEmpty(q.Code.Code));

        var uniqueSpecialties = allQualifications
            .GroupBy(q => new { q.Code.Code, q.Code.DisplayName })
            .Select(g => new SpecialtyDto
            {
                Code = g.Key.Code,
                DisplayName = g.Key.DisplayName ?? "Без названия"
            })
            .OrderBy(s => s.DisplayName)
            .ToList();

        return uniqueSpecialties;
    }

    public async Task<IReadOnlyList<PractitionerDto>> GetBySpecialtyAsync(string specialtyCode, CancellationToken ct = default)
    {
        var practitioners = await _repository.GetBySpecialtyAsync(specialtyCode, ct);

        return practitioners.Select(p =>
        {
            var primaryName = p.PractitionerNames.FirstOrDefault();

            return new PractitionerDto
            {
                Id = p.Id,
                IsActive = p.IsActive,
                Gender = p.GenderType.ToString(),
                BirthDate = p.BirthDate,

                FullName = new HumanNameDto
                {
                    Family = primaryName?.Family ?? "Не указана",
                    Name = primaryName?.Name ?? "Не указано",
                    Patronymic = primaryName?.Patronymic
                },

                Identifiers = p.PractitionerIdentifiers.Select(i => new PractitionerIdentifierDto
                {
                    System = i.IdentifierType?.DisplayName ?? "Неизвестно",
                    Value = i.Value
                }).ToList(),

                Qualifications = p.PractitionerQualification.Select(q => new QualificationDto
                {
                    Code = q.Code.Code,
                    DisplayName = q.Code.DisplayName ?? "Без названия",
                    Issuer = q.Issuer,
                    PeriodStart = q.PeriodStart,
                    PeriodEnd = q.PeriodEnd
                }).ToList()
            };
        }).ToList();
    }
}