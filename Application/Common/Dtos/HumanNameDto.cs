namespace Application.Common.Dtos
{
    public class HumanNameDto
    {
        public string Family { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string Display => $"{Family} {Name} {Patronymic}".Trim();
    }
}
