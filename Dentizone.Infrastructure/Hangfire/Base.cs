using Hangfire;
using System.Linq.Expressions;
using Dentizone.Application.Interfaces;

namespace Dentizone.Infrastructure.Hangfire
{
    public class HangfireBackgroundJobService : IBackgroundJobService
    {
        public void Enqueue<T>(Expression<Func<T, Task>> methodCall)
        {
            BackgroundJob.Enqueue<T>(methodCall);
        }
    }
}