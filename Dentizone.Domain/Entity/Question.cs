using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Question : IBaseEntity
    {
        public string PostId { get; set; }
        public Post Post { get; set; }
        public string AskerId { get; set; }
        public virtual IAppUser User { get; set; }
        public string Text { get; set; }
        public QuestionStatus Status { get; set; }

        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Answer Answer { get; set; }
    }
}