namespace Application.Conditions.Dtos
{
    public class ConditionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
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
