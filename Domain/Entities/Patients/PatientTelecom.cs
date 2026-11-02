using Domain.Entities.Common;

namespace Domain.Entities.Patients
{
    public enum TelecomType
    {
        phone, 
        email
    }

    public class PatientTelecom : BaseEntity
    {
        public TelecomType TelecomType { get; set; }

        public string Value { get; set; } = string.Empty;

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}
