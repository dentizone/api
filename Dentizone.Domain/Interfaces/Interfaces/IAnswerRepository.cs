using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    internal interface IAnswerRepository : IBaseRepo<Answer>
    {
        Task<Answer> UpdateAsync(Answer entity);
        Task<Answer?> DeleteAsync(string id);
    }
}