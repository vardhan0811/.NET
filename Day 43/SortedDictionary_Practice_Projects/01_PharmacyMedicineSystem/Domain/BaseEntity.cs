namespace Domain
{
    public abstract class BaseEntity
    {
        public string Id { get; set; } = string.Empty;
        public abstract void Validate();
    }
}
