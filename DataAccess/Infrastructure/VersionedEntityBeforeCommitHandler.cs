using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Infrastructure
{
    public class VersionedEntityBeforeCommitHandler : IBeforeCommitHandler
    {
        public void Execute(ApplicationContext context)
        {
            foreach (EntityEntry entry in context.ChangeTracker.Entries())
            {
                var isAllowedState = entry.State is EntityState.Modified or EntityState.Deleted;
                if (entry.Entity is BaseVersionedEntity entity && isAllowedState)
                {
                    entry.Property(nameof(BaseVersionedEntity.RowVersion)).OriginalValue = entity.RowVersion;
                }
            }
        }
    }
}
