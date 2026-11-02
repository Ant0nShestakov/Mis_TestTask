using Application.Common.Dtos;

namespace Application.Conditions.Dtos
{
    public class ConditionDto
    {
        public Guid Id { get; set; }

        public CodeableConceptDto Code { get; set; } = new();
        public string ClinicalStatus { get; set; } = string.Empty;
        public string VerificationStatus { get; set; } = string.Empty;
        public DateTimeOffset RecordedDate { get; set; }
        public DateTimeOffset? OnsetDateTime { get; set; }

        public string PatientFullName { get; set; } = string.Empty;
        public string? RecorderFullName { get; set; }
    }

    public class ConditionShortDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}
