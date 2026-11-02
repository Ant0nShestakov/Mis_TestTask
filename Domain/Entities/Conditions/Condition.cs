using Domain.Common;
using Domain.Entities.Common;
using Domain.Entities.Encounters;
using Domain.Entities.Patients;
using Domain.Entities.Practitioners;

namespace Domain.Entities.Conditions
{
    public enum ConditionClinicalStatus
    {
        Active,
        Recurrence,
        Relapse,
        Inactive,
        Remission,
        Resolved
    }

    public enum ConditionVerificationStatus
    {
        Unconfirmed,
        Provisional,
        Differential,
        Confirmed,
        Refuted,
        EnteredInError
    }

    public enum ConditionSeverity
    {
        Mild,
        Moderate,
        Severe
    }

    public class Condition : BaseEntity
    {
        public ConditionClinicalStatus ClinicalStatus { get; set; }

        public ConditionVerificationStatus VerificationStatus { get; set; }

        public ConditionSeverity? Severity { get; set; }

        // Диагноз по МКБ-10
        public CodeableConcept Code { get; set; } = new();

        public DateTimeOffset? OnsetDateTime { get; set; }

        public DateTimeOffset? AbatementDateTime { get; set; }

        public DateTimeOffset RecordedDate { get; set; } = DateTimeOffset.UtcNow;

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public Guid? EncounterId { get; set; }
        public Encounter? Encounter { get; set; }

        public Guid? RecorderId { get; set; }
        public Practitioner? Recorder { get; set; }
    }
}
