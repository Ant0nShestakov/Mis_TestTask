namespace Application.Observation.Dtos
{
    public class ObservationDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal? ValueQuantity { get; set; }
        public string? ValueUnit { get; set; }
        public string? ValueString { get; set; }
    }
}
