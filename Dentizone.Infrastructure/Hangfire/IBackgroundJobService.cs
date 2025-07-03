using System.Linq.Expressions;

namespace Dentizone.Application.Interfaces
{
    public interface IBackgroundJobService
    {
        void Enqueue<T>(Expression<Func<T, Task>> methodCall);
    }
}