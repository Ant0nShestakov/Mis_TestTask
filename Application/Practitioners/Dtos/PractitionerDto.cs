using Application.Common.Dtos;

namespace Application.Practitioners.Dtos
{
    public class PractitionerDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }

        public HumanNameDto FullName { get; set; } = new();

        public List<PractitionerIdentifierDto> Identifiers { get; set; } = new();

        public List<QualificationDto> Qualifications { get; set; } = new();
    }

    public class PractitionerIdentifierDto
    {
        public string System { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class QualificationDto
    {
        public string Code { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public DateTimeOffset? PeriodStart { get; set; }
        public DateTimeOffset? PeriodEnd { get; set; }
    }
}
