using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Answer : IBaseEntity, IUpdatable, IDeletable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string QuestionId { get; set; }
        public required string Text { get; set; }
        public AnswerStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual required Question Question { get; set; }
    }
}