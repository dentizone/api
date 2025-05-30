using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;
using AppUser = Dentizone.Domain.Entity.AppUser;

namespace Dentizone.Domain.Entity
{
    public class Question : IBaseEntity
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string AskerId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public Post Post { get; set; }
        public virtual Answer Answer { get; set; }
        public QuestionStatus Status { get; set; }
        public virtual AppUser User { get; set; }
    }
}