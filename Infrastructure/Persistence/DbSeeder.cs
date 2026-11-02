using Domain.Common;
using Domain.Entities.Conditions;
using Domain.Entities.Encounters;
using Domain.Entities.Observations;
using Domain.Entities.Patients;
using Domain.Entities.Practitioners;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(TestTaskDbContext db)
    {
        if (await db.Patients.AnyAsync()) return;

        var doctor = new Practitioner
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            GenderType = GenderType.Male,
            BirthDate = new DateOnly(1980, 5, 20),
            PractitionerNames = new List<PractitionerName>
            {
                new() { Family = "Иванов", Name = "Иван", Patronymic = "Иванович" }
            },
            PractitionerQualification = new List<PractitionerQualification>
            {
                new()
                {
                    Code = new CodeableConcept { Code = "therapist", DisplayName = "Терапевт" }
                }
            }
        };
        db.Practitioners.Add(doctor);

        var patient = new Patient
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            GenderType = GenderType.Male,
            BirthDate = new DateOnly(1990, 1, 1),
            MaritalStatus = new CodeableConcept { Code = "S", DisplayName = "Холост" },
            PatientNames = new List<PatientName>
            {
                new() { Family = "Петров", Name = "Петр", Patronymic = "Петрович" }
            },
            PatientIdentifiers = new List<PatientIdentifiers>
            {
                new() { IdentifierType = new CodeableConcept { Code = "snils", DisplayName = "СНИЛС" }, Value = "123-456-789 00" }
            },
            PatientTelecoms = new List<PatientTelecom>
            {
                new() { TelecomType = TelecomType.phone, Value = "+7 999 123-45-67" }
            }
        };
        db.Patients.Add(patient);

        var encounter = new Encounter
        {
            Id = Guid.NewGuid(),
            Status = EncounterStatus.Finished,
            Class = EncounterClass.Ambulatory,
            PeriodStart = DateTimeOffset.UtcNow.AddDays(-2),
            PeriodEnd = DateTimeOffset.UtcNow.AddDays(-2).AddHours(1),
            Patient = patient,
            Practitioner = doctor
        };
        db.Encounters.Add(encounter);

        var condition = new Condition
        {
            Id = Guid.NewGuid(),
            ClinicalStatus = ConditionClinicalStatus.Active,
            VerificationStatus = ConditionVerificationStatus.Confirmed,
            Code = new CodeableConcept { Code = "J00", DisplayName = "Острый назофарингит (Насморк)" },
            Patient = patient,
            Encounter = encounter,
            Recorder = doctor,
            RecordedDate = DateTimeOffset.UtcNow
        };
        db.Conditions.Add(condition);

        var observation = new Observation
        {
            Id = Guid.NewGuid(),
            ObservationStatus = ObservationStatus.Final,
            Code = new CodeableConcept { Code = "8310-5", DisplayName = "Температура тела" },
            ValueQuantity = 37.5m,
            ValueUnit = "C",
            EffectiveDateTime = DateTimeOffset.UtcNow.AddDays(-2),
            Patient = patient,
            Encounter = encounter
        };
        db.Observations.Add(observation);

        await db.SaveChangesAsync();
    }
}