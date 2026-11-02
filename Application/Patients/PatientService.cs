using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Conditions.Dtos;
using Application.Encounters.Dtos;
using Application.Observation.Dtos;
using Application.Patients.Dtos;
using Microsoft.Extensions.Logging;

namespace Application.Patients;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepo;
    private readonly IConditionRepository _conditionRepo;
    private readonly IEncounterRepository _encounterRepo;
    private readonly IObservationRepository _observationRepo;
    private readonly ILogger<PatientService> _logger;

    public PatientService(
        IPatientRepository patientRepo,
        IConditionRepository conditionRepo,
        IEncounterRepository encounterRepo,
        IObservationRepository observationRepo,
        ILogger<PatientService> logger)
    {
        _patientRepo = patientRepo;
        _conditionRepo = conditionRepo;
        _encounterRepo = encounterRepo;
        _observationRepo = observationRepo;
        _logger = logger;
    }

    public async Task<IReadOnlyList<PatientShortDto>> GetAllAsync(CancellationToken ct = default)
    {
        var patients = await _patientRepo.GetAllAsync(ct);

        return patients.Select(p => new PatientShortDto
        {
            Id = p.Id,
            FullName = BuildFullName(p),
            BirthDate = p.BirthDate
        }).ToList();
    }

    public async Task<PatientDTO> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        _logger.LogInformation("Getting full patient card for {PatientId}", id);

        var patient = await _patientRepo.GetByIdAsync(id, ct);
        if (patient == null)
        {
            _logger.LogWarning("Patient {PatientId} not found", id);
            throw new NotFoundException($"Пациент с ID {id} не найден.");
        }

        var conditions = await _conditionRepo.GetByPatientIdAsync(id, ct);
        var encounters = await _encounterRepo.GetByPatientIdAsync(id, ct);

        var encounterIds = encounters.Select(e => e.Id).ToList();
        var allObservations = await _observationRepo.GetByEncounterIdsAsync(encounterIds, ct);
        var observationsLookup = allObservations.ToLookup(o => o.EncounterId);

        var primaryName = patient.PatientNames.FirstOrDefault();

        return new PatientDTO
        {
            Id = patient.Id,
            IsActive = patient.IsActive,
            Gender = patient.GenderType.ToString(),
            BirthDate = patient.BirthDate,
            IsDeceased = patient.IsDeceased,
            DeceasedDateTime = patient.DeceasedDateTime,
            MaritalStatus = patient.MaritalStatus != null ? new CodeableConceptDto
            {
                Code = patient.MaritalStatus.Code,
                DisplayName = patient.MaritalStatus.DisplayName
            } : null,

            FullName = new HumanNameDto
            {
                Family = primaryName?.Family ?? "Не указана",
                Name = primaryName?.Name ?? "Не указано",
                Patronymic = primaryName?.Patronymic
            },

            Identifiers = patient.PatientIdentifiers.Select(i => new PatientIdentifierDto
            {
                System = i.IdentifierType?.DisplayName ?? "Неизвестно",
                Value = i.Value
            }).ToList(),

            Telecoms = patient.PatientTelecoms.Select(t => new PatientTelecomDto
            {
                Type = t.TelecomType.ToString(),
                Value = t.Value
            }).ToList(),

            Addresses = patient.PatientAddresses.Select(a => new PatientAddressDto
            {
                Type = a.AddressType.ToString(),
                Address = a.FullAddress
            }).ToList(),

            Conditions = conditions.Select(c => new ConditionShortDto
            {
                Id = c.Id,
                Code = c.Code.Code,
                DisplayName = c.Code.DisplayName ?? "Без названия"
            }).ToList(),

           Encounters = encounters.Select(e => new EncounterDto
            {
                Id = e.Id,
                PeriodStart = e.PeriodStart,
                Status = e.Status.ToString(),
                PractitionerName = e.Practitioner?.PractitionerNames?.FirstOrDefault()?.Family ?? "Неизвестный врач",
                Observations = observationsLookup[e.Id].Select(o => new ObservationDto
                {
                    Id = o.Id,
                    Code = o.Code.DisplayName ?? o.Code.Code,
                    ValueQuantity = o.ValueQuantity,
                    ValueUnit = o.ValueUnit,
                    ValueString = o.ValueString
                }).ToList()
            }).ToList()
        };
    }

    private static string BuildFullName(Domain.Entities.Patients.Patient patient)
    {
        var name = patient.PatientNames?.FirstOrDefault();
        if (name == null) return "Имя не указано";
        return $"{name.Family} {name.Name} {name.Patronymic}".Trim();
    }
}