using Application.Common.Interfaces;
using Application.Conditions.Dtos;

namespace Application.Conditions;

public class ConditionService : IConditionService
{
    private readonly IConditionRepository _repository;

    public ConditionService(IConditionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ConditionDto>> GetAllAsync(CancellationToken ct = default)
    {
        var conditions = await _repository.GetAllAsync(ct);

        return conditions.Select(c => new ConditionDto
        {
            Id = c.Id,
            Code = c.Code.Code,
            DisplayName = c.Code?.DisplayName ?? "Без названия",
            PatientFullName = c.Patient?.PatientNames?.FirstOrDefault() != null
                ? $"{c.Patient.PatientNames.First().Family} {c.Patient.PatientNames.First().Name}".Trim()
                : "Неизвестный пациент",
            RecorderFullName = c.Recorder?.PractitionerNames?.FirstOrDefault() != null
                ? $"{c.Recorder.PractitionerNames.First().Family} {c.Recorder.PractitionerNames.First().Name}".Trim()
                : null
        }).ToList();
    }
}