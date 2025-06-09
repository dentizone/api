using Dentizone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Dentizone.Infrastructure.Persistence.Interceptors;

public class BaseEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker.Entries<IBaseEntity>();
        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    {
                        entry.Entity.CreatedAt = now;

                        if (entry.Entity is IUpdatable updatable)
                            updatable.UpdatedAt = now;

                        if (string.IsNullOrEmpty(entry.Entity.Id))
                            entry.Entity.Id = Guid.NewGuid().ToString();

                        if (entry.Entity is IDeletable deletable)
                            deletable.IsDeleted = false;

                        break;
                    }
                case EntityState.Modified:
                    {
                        if (entry.Entity is IUpdatable updatable)
                            updatable.UpdatedAt = now;

                        break;
                    }
                case EntityState.Deleted:
                    {
                        if (entry.Entity is IDeletable deletable)
                        {
                            deletable.IsDeleted = true;
                            if (deletable is IUpdatable updatableDeletable)
                                updatableDeletable.UpdatedAt = now;
                            entry.State = EntityState.Modified;
                        }

                        break;
                    }
            }
        }
    }
}