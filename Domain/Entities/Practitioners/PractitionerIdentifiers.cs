using Domain.Common;
using Domain.Entities.Common;

namespace Domain.Entities.Practitioners
{
    public class PractitionerIdentifiers : BaseEntity
    {
        public CodeableConcept IdentifierType { get; set; } = new();

        public string Value { get; set; } = string.Empty;

        public Guid PractitionerId { get; set; }
        public Practitioner Practitioner { get; set; } = null!;
    }
}
