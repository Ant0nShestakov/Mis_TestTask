using Domain.Entities.Common;

namespace Domain.Entities.Patients
{
    public class PatientName : HumanNameBase
    {
        public Guid PatientId { get; set; }

        public Patient Patient { get; set; } = null!;
    }
}
