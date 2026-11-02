using Domain.Common;
using Domain.Entities.Common;

namespace Domain.Entities.Practitioners
{
    public class PractitionerQualification : BaseEntity
    {
        // Идентификатор документа (номер диплома/сертификата)
        public string IdentifierValue { get; set; } = string.Empty;

        // Код специальности
        public CodeableConcept Code { get; set; } = new();

        // Кем выдан
        public string Issuer { get; set; } = string.Empty;

        public DateTimeOffset? PeriodStart { get; set; }

        public DateTimeOffset? PeriodEnd { get; set; }

        public Guid PractitionerId { get; set; }
        public Practitioner Practitioner { get; set; } = null!;
    }
}
