using Domain.Entities.Common;

namespace Domain.Entities.Patients
{
    public enum AddressType
    {
        Home, 

    }

    public class PatientAddresses : BaseEntity
    {
        public AddressType AddressType { get; set; }

        public string FullAddress { get; set; } = string.Empty;

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}
