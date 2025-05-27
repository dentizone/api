using Dentizone.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Dentizone.Infrastructure.Persistence.Interceptors;

internal class BaseEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var context = eventData.Context;

        if (context == null) return base.SavingChanges(eventData, result);

        var entries = context.ChangeTracker.Entries<IBaseEntity>();

        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    {
                        entry.Entity.CreatedAt = now;
                        entry.Entity.UpdatedAt = now;

                        if (string.IsNullOrEmpty(entry.Entity.Id))
                        {
                            entry.Entity.Id = Guid.NewGuid().ToString();
                        }

                        entry.Entity.IsDeleted = false;
                        break;
                    }
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    break;
            }
        }

        return base.SavingChanges(eventData, result);
    }

}

