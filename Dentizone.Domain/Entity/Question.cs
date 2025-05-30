using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Question : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PostId { get; set; }
        public string AskerId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Post Post { get; set; }
        public virtual Answer Answer { get; set; }
        public QuestionStatus Status { get; set; }
        public virtual AppUser User { get; set; }
    }
}