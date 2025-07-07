using System.Linq.Expressions;

namespace Dentizone.Domain.Interfaces
{
    public interface IBackgroundJobService
    {
        void Enqueue<T>(Expression<Func<T, Task>> methodCall);
    }
}