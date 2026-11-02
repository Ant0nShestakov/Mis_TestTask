namespace Domain.Common
{
    public class CodeableConcept
    {
        // Код МКБ
        public string Code { get; set; } = string.Empty;

        // Система кодирования
        public string? System { get; set; }

        public string? DisplayName { get; set; }
    }
}
