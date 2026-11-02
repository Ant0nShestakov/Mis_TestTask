namespace Domain.Entities.Common
{
    public abstract class HumanNameBase : BaseEntity
    {
        public string? Name { get; set; }

        public string? Family { get; set; }

        public string? Patronymic { get; set; }
    }
}
