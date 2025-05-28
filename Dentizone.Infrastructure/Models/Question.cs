using Dentizone.Application.Interfaces;
using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class Question : IBaseEntity
    {
        public string PostId { get; set; }
        public Post Post { get; set; }
        public string AskerId { get; set; }
        public AppUser User { get; set; }
        public string Text { get; set; }
        public DateTime AddedAt { get; set; }
        public QuestionStatus Status { get; set; }

        // public Answer answer { get; set; }

        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
