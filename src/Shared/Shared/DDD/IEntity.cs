namespace Shared.DDD
{
    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; } 
    }
    public interface IEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
    }
}
