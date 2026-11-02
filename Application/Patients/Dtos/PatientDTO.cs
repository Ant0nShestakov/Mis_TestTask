using Application.Common.Dtos;
using Application.Conditions.Dtos;
using Application.Encounters.Dtos;

namespace Application.Patients.Dtos
{

    public class PatientDTO
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }

        public bool IsDeceased { get; set; }
        public DateTimeOffset? DeceasedDateTime { get; set; }

        public CodeableConceptDto? MaritalStatus { get; set; }

        public HumanNameDto FullName { get; set; } = new();

        public List<PatientIdentifierDto> Identifiers { get; set; } = new();

        public List<PatientTelecomDto> Telecoms { get; set; } = new();

        public List<PatientAddressDto> Addresses { get; set; } = new();

        public List<ConditionShortDto> Conditions { get; set; } = new();
        public List<EncounterDto> Encounters { get; set; } = new();
    }

    public class PatientShortDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
    }

    public class PatientIdentifierDto
    {
        public string System { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class PatientTelecomDto
    {
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class PatientAddressDto
    {
        public string Type { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
