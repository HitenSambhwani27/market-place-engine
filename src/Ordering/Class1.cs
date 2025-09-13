namespace Ordering
{
    public interface IEntity<T>: Enitity
    {
        public T Id { get; set; }
    }
    public interface Enitity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public string? CreatedBy { get; set; }

    }
}
