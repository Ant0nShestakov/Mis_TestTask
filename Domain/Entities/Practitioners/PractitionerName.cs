using Domain.Entities.Common;

namespace Domain.Entities.Practitioners
{
    public class PractitionerName : HumanNameBase
    {
        public Guid PractitionerId { get; set; }
        public Practitioner Practitioner { get; set; } = null!;
    }
}
