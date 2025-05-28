using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class Answer : IBaseEntity
    {
        public string Id { get; set; }
        public string QuestionId { get; set; }
        public string Text { get; set; }
        public AnswerStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Question Question { get; set; }  
    }

}
