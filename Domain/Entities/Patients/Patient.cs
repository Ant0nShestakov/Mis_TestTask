using Domain.Common;
using Domain.Entities.Common;


namespace Domain.Entities.Patients
{

    public class Patient : BaseEntity
    {
        public bool IsActive { get; set; }

        public GenderType GenderType { get; set; }

        public DateOnly BirthDate { get; set; }

        public CodeableConcept MaritalStatus { get; set; } = new ();

        public bool IsDeceased { get; set; }

        public DateTimeOffset? DeceasedDateTime { get; set; }

        public List<PatientName> PatientNames { get; set; } = [];

        public List<PatientIdentifiers> PatientIdentifiers { get; set; } = [];

        public List<PatientTelecom> PatientTelecoms { get; set; } = [];

        public List<PatientAddresses> PatientAddresses { get; set; } = [];
    }
}
