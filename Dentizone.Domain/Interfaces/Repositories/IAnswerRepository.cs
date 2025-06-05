using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IAnswerRepository : IBaseRepo<Answer>
    {
        Task<Answer> UpdateAsync(Answer entity);
        Task<Answer?> DeleteAsync(string id);
    }
}