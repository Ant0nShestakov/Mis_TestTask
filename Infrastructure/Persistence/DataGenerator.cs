using Bogus;
using Bogus.DataSets; // Необходимо для Name.Gender
using Domain.Common;
using Domain.Entities.Conditions;
using Domain.Entities.Encounters;
using Domain.Entities.Observations;
using Domain.Entities.Patients;
using Domain.Entities.Practitioners;

namespace Infrastructure.Persistence;

/// <summary>
/// Сделал с нейронкой чтобы просто нагенерить данных в бд
/// </summary>
public static class DataGenerator
{
    /// <summary>
    /// Генерация отчества в зависимости от пола
    /// </summary>
    private static string GetRandomPatronymic(Faker f, GenderType gender)
    {
        var malePatronymics = new[] { "Иванович", "Петрович", "Сергеевич", "Алексеевич", "Дмитриевич", "Андреевич" };
        var femalePatronymics = new[] { "Ивановна", "Петровна", "Сергеевна", "Алексеевна", "Дмитриевна", "Андреевна" };

        return gender == GenderType.Male
            ? f.PickRandom(malePatronymics)
            : f.PickRandom(femalePatronymics);
    }

    public static (List<Practitioner> doctors, List<Patient> patients, List<Encounter> encounters, List<Condition> conditions, List<Observation> observations) GenerateData()
    {
        Randomizer.Seed = new Random(8675309);

        var specialtyCodes = new[] { "therapist", "cardiologist", "surgeon", "neurologist", "pediatrician" };
        var specialtyNames = new[] { "Терапевт", "Кардиолог", "Хирург", "Невролог", "Педиатр" };

        // 1. Генерация Врачей (10 человек)
        var doctorFaker = new Faker<Practitioner>("ru")
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.IsActive, f => true)
            .RuleFor(p => p.GenderType, f => f.PickRandom<GenderType>())
            .RuleFor(p => p.BirthDate, f => DateOnly.FromDateTime(f.Date.Past(50, DateTime.Now.AddYears(-25))))
            .RuleFor(p => p.PractitionerNames, (f, p) =>
            {
                var bogusGender = p.GenderType == GenderType.Male ? Name.Gender.Male : Name.Gender.Female;
                return new List<PractitionerName>
                {
                    new()
                    {
                        Family = f.Name.LastName(bogusGender),
                        Name = f.Name.FirstName(bogusGender),
                        Patronymic = GetRandomPatronymic(f, p.GenderType)
                    }
                };
            })
            .RuleFor(p => p.PractitionerQualification, f =>
            {
                var index = f.Random.Int(0, specialtyCodes.Length - 1);
                return new List<PractitionerQualification>
                {
                    new() {
                        Code = new CodeableConcept { Code = specialtyCodes[index], DisplayName = specialtyNames[index] },
                        Issuer = "Минздрав"
                    }
                };
            })
            .RuleFor(p => p.PractitionerIdentifiers, f => new List<PractitionerIdentifiers>
            {
                new() { IdentifierType = new CodeableConcept { Code = "license", DisplayName = "Лицензия" }, Value = f.Random.Replace("Л-######") }
            });

        var doctors = doctorFaker.Generate(10);

        // 2. Генерация Пациентов (30 человек)
        var patientFaker = new Faker<Patient>("ru")
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.IsActive, f => true)
            .RuleFor(p => p.GenderType, f => f.PickRandom<GenderType>())
            .RuleFor(p => p.BirthDate, f => DateOnly.FromDateTime(f.Date.Past(80, DateTime.Now)))
            .RuleFor(p => p.MaritalStatus, f => new CodeableConcept { Code = "S", DisplayName = f.PickRandom("Холост", "Женат", "Разведен") })
            .RuleFor(p => p.PatientNames, (f, p) =>
            {
                var bogusGender = p.GenderType == GenderType.Male ? Name.Gender.Male : Name.Gender.Female;
                return new List<PatientName>
                {
                    new()
                    {
                        Family = f.Name.LastName(bogusGender),
                        Name = f.Name.FirstName(bogusGender),
                        Patronymic = GetRandomPatronymic(f, p.GenderType)
                    }
                };
            })
            .RuleFor(p => p.PatientIdentifiers, f => new List<PatientIdentifiers>
            {
                new() { IdentifierType = new CodeableConcept { Code = "snils", DisplayName = "СНИЛС" }, Value = f.Random.Replace("###-###-### ##") }
            })
            .RuleFor(p => p.PatientTelecoms, f => new List<PatientTelecom>
            {
                new() { TelecomType = TelecomType.phone, Value = f.Phone.PhoneNumber("+7 (###) ###-####") }
            })
            .RuleFor(p => p.PatientAddresses, f => new List<PatientAddresses>
            {
                new() { AddressType = AddressType.Home, FullAddress = $"г. {f.Address.City()}, ул. {f.Address.StreetName()}, д. {f.Address.BuildingNumber()}" }
            });

        var patients = patientFaker.Generate(30);

        // 3. Генерация Приемов (Encounters)
        var encounterFaker = new Faker<Encounter>("ru")
            .RuleFor(e => e.Id, f => Guid.NewGuid())
            .RuleFor(e => e.Status, f => f.PickRandom<EncounterStatus>())
            .RuleFor(e => e.Class, f => f.PickRandom<EncounterClass>())
            .RuleFor(e => e.PeriodStart, f => f.Date.PastOffset(1).ToUniversalTime())
            .RuleFor(e => e.Patient, f => f.Random.ListItem(patients))
            .RuleFor(e => e.Practitioner, f => f.Random.ListItem(doctors));

        var encounters = encounterFaker.Generate(40);

        // Проставляем PeriodEnd для завершенных приемов
        foreach (var e in encounters.Where(x => x.Status == EncounterStatus.Finished))
        {
            e.PeriodEnd = e.PeriodStart.AddMinutes(new Faker().Random.Int(15, 120)).ToUniversalTime();
        }

        // 4. Генерация Диагнозов (Conditions)
        var conditionFaker = new Faker<Condition>("ru")
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.ClinicalStatus, f => f.PickRandom<ConditionClinicalStatus>())
            .RuleFor(c => c.VerificationStatus, ConditionVerificationStatus.Confirmed)
            .RuleFor(c => c.RecordedDate, f => f.Date.PastOffset(1).ToUniversalTime())
            .RuleFor(c => c.Patient, f => f.Random.ListItem(patients))
            .RuleFor(c => c.Recorder, f => f.Random.ListItem(doctors))
            .RuleFor(c => c.Code, f => new CodeableConcept
            {
                Code = f.Random.Replace("A##"),
                DisplayName = f.PickRandom("ОРВИ", "Ангина", "Гастрит", "Гипертония", "Мигрень")
            });

        var conditions = conditionFaker.Generate(50);

        // Привязываем диагнозы к приемам
        foreach (var cond in conditions)
        {
            var encounter = encounters.FirstOrDefault(e => e.Patient.Id == cond.Patient.Id);
            cond.Encounter = encounter;
        }

        // 5. Генерация Замеров (Observations)
        var observationFaker = new Faker<Observation>("ru")
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.ObservationStatus, ObservationStatus.Final)
            .RuleFor(o => o.EffectiveDateTime, f => f.Date.PastOffset(1).ToUniversalTime())
            .RuleFor(o => o.ValueQuantity, f => f.Random.Decimal(35, 40))
            .RuleFor(o => o.ValueUnit, "C")
            .RuleFor(o => o.Code, f => new CodeableConcept { Code = "8310-5", DisplayName = "Температура тела" })
            .RuleFor(o => o.Patient, f => f.Random.ListItem(patients))
            .RuleFor(o => o.Encounter, f => f.Random.ListItem(encounters));

        var observations = observationFaker.Generate(60);

        return (doctors, patients, encounters, conditions, observations);
    }
}