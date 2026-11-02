using Application.Common.Dtos;
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

        return conditions.Select(c =>
        {
            var patientName = c.Patient?.PatientNames?.FirstOrDefault();
            var recorderName = c.Recorder?.PractitionerNames?.FirstOrDefault();

            return new ConditionDto
            {
                Id = c.Id,
                Code = new CodeableConceptDto
                {
                    Code = c.Code.Code,
                    DisplayName = c.Code.DisplayName ?? "Без названия"
                },
                ClinicalStatus = c.ClinicalStatus.ToString(),
                VerificationStatus = c.VerificationStatus.ToString(),
                RecordedDate = c.RecordedDate,
                OnsetDateTime = c.OnsetDateTime,

                PatientFullName = patientName != null
                    ? $"{patientName.Family} {patientName.Name} {patientName.Patronymic}".Trim()
                    : "Неизвестный пациент",

                RecorderFullName = recorderName != null
                    ? $"{recorderName.Family} {recorderName.Name} {recorderName.Patronymic}".Trim()
                    : null
            };
        }).ToList();
    }
}
