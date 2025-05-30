using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Answer : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string QuestionId { get; set; }
        public string Text { get; set; }
        public AnswerStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Question Question { get; set; }
    }
}