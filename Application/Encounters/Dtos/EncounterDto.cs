using Application.Observation.Dtos;

namespace Application.Encounters.Dtos
{
    public class EncounterDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset PeriodStart { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PractitionerName { get; set; } = string.Empty;
        public List<ObservationDto> Observations { get; set; } = new();
    }
}
