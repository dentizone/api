using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class Question
    {
        public string id { get; set; }
        public string post_id { get; set; }
        public Post Post { get; set; }
        public string asker_id { get; set; }
        public AppUser user { get; set; }
        public string text { get; set; }
        public DateTime added_at { get; set; }
        public QuestionStatus status { get; set; }
        public Boolean is_deleted { get; set; }

        // public Answer answer { get; set; }

    }
}
