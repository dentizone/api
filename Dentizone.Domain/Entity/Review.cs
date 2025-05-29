using Dentizone.Application.Interfaces;
using Dentizone.Domain;

namespace Dentizone.Infrastructure.Models
{
    internal class Review : IBaseEntity
    {

        public string Id { get; set; }
        public string UserId { get; set; }
        public int Stars { get; set; }
        public string? Text { get; set; }
        public string OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Order Order { get; set; }
        public bool IsDeleted { get; set; }
    }
}

