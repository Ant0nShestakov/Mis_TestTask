using Domain.Common;
using Domain.Entities.Common;

namespace Domain.Entities.Practitioners
{
    public class Practitioner : BaseEntity
    {
        public bool IsActive { get; set; }

        public GenderType GenderType { get; set; }

        public DateOnly BirthDate { get; set; }

        public List<PractitionerName> PractitionerNames { get; set; } = [];

        public List<PractitionerIdentifiers> PractitionerIdentifiers { get; set; } = [];

        public List<PractitionerQualification> PractitionerQualification { get; set; } = [];
    }
}
