namespace Dentizone.Domain.Interfaces
{
    public interface IBaseEntity
    {
        public string Id { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}