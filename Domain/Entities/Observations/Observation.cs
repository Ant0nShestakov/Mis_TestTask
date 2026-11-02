using Domain.Common;
using Domain.Entities.Common;
using Domain.Entities.Encounters;
using Domain.Entities.Patients;


namespace Domain.Entities.Observations
{
    public enum ObservationStatus
    {
        Registered, 
        Preliminary, 
        Final, 
        Amended, 
        Cancelled
    }

    public class Observation : BaseEntity
    {
        public ObservationStatus ObservationStatus { get; set; }

        public CodeableConcept Code { get; set; } = new();

        public DateTimeOffset EffectiveDateTime { get; set; }

        // Для числовых значений (давление, пульс, вес)
        public decimal? ValueQuantity { get; set; }

        // Единица измерения (мм рт. ст., кг, г/л)
        public string? ValueUnit { get; set; }

        // Для текстовых результатов (положительно/отрицательно, обнаружено)
        public string? ValueString { get; set; }

        public string? ValueDisplay { get; set; }

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public Guid? EncounterId { get; set; }
        public Encounter? Encounter { get; set; }
    }
}
