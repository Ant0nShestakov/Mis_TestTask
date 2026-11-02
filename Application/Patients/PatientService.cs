using Application.Common.Interfaces;
using Application.Conditions.Dtos;
using Application.Encounters.Dtos;
using Application.Observation.Dtos;
using Application.Patients.Dtos;
using Microsoft.Extensions.Logging;

namespace Application.Patients
{
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

            // 3. Оптимизация N+1: получаем все замеры для всех приемов ОДНИМ запросом
            var encounterIds = encounters.Select(e => e.Id).ToList();
            var allObservations = await _observationRepo.GetByEncounterIdsAsync(encounterIds, ct);
            var observationsLookup = allObservations.ToLookup(o => o.EncounterId);


            return new PatientDTO
            {
                Id = patient.Id,
                IsActive = patient.IsActive,
                Gender = patient.GenderType.ToString(),
                BirthDate = patient.BirthDate,
                IsDeceased = patient.IsDeceased,

                // Безопасное обращение к MaritalStatus
                MaritalStatus = patient.MaritalStatus?.DisplayName,

                FullName = BuildFullName(patient),

                // Безопасное обращение к IdentifierType
                Identifiers = patient.PatientIdentifiers
        .Select(i => $"{i.IdentifierType?.DisplayName ?? "Неизвестный тип"}: {i.Value}")
        .ToList(),

                Phones = patient.PatientTelecoms.Select(t => t.Value).ToList(),

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

                    // Безопасное получение имени врача
                    PractitionerName = e.Practitioner?.PractitionerNames?.FirstOrDefault()?.Family ?? "Неизвестный врач",

                    Observations = observationsLookup[e.Id].Select(o => new ObservationDto
                    {
                        Id = o.Id,
                        Code = o.Code.DisplayName ?? o.Code.Code, // Если Code тоже Owned, он не будет null
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
}
