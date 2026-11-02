namespace Application.Practitioners.Dtos
{
    public class PractitionerDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public List<string> Specialties { get; set; } = new();
    }
}
