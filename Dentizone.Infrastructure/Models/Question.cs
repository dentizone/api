using Dentizone.Application.Interfaces;
using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class Question : IBaseEntity
    {
        public string post_id { get; set; }
        public Post Post { get; set; }
        public string asker_id { get; set; }
        public AppUser user { get; set; }
        public string text { get; set; }
        public DateTime added_at { get; set; }
        public QuestionStatus status { get; set; }

        // public Answer answer { get; set; }

        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
