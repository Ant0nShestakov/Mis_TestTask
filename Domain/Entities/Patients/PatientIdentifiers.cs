using Domain.Common;
using Domain.Entities.Common;

namespace Domain.Entities.Patients
{
    public class PatientIdentifiers : BaseEntity
    {
        public CodeableConcept IdentifierType { get; set; } = new();

        public string Value { get; set; } = string.Empty;

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}
