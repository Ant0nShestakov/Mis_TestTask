using Domain.Common;
using Domain.Entities.Common;
using Domain.Entities.Patients;
using Domain.Entities.Practitioners;

namespace Domain.Entities.Encounters
{
    public enum EncounterStatus
    {
        Planned,
        Arrived,
        InProgress,
        Finished,
        Cancelled
    }

    public enum EncounterClass
    {
        Ambulatory, // AMB (Поликлиника)
        Inpatient,  // IMP (Стационар)
        Emergency   // EMER (Скорая)
    }

    public class Encounter : BaseEntity
    {
        public EncounterStatus Status { get; set; }

        public EncounterClass Class { get; set; }

        public CodeableConcept EncounterType { get; set; } = new();

        public DateTimeOffset PeriodStart { get; set; }
        public DateTimeOffset? PeriodEnd { get; set; }

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public Guid PractitionerId { get; set; }
        public Practitioner Practitioner { get; set; } = null!;
    }
}
