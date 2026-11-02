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

        public string? MaritalStatus { get; set; }

        public string FullName { get; set; } = string.Empty;

        public List<string> Identifiers { get; set; } = new();
        public List<string> Phones { get; set; } = new();

        public List<ConditionShortDto> Conditions { get; set; } = new();

        public List<EncounterDto> Encounters { get; set; } = new();
    }

    public class PatientShortDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
    }
}
