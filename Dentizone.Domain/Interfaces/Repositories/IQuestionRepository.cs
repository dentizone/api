using Dentizone.Domain.Entity;

namespace Dentizone.Domain.Interfaces.Repositories
{
    public interface IQuestionRepository : IBaseRepo<Question>
    {
        Task<Question> UpdateAsync(Question entity);
        Task<Question?> DeleteAsync(string id);
    }
}