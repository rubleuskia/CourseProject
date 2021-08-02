using System.Linq;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Infrastructure
{
    public class VersionedEntityBeforeCommitHandler : IBeforeCommitHandler
    {
        public void Execute(ApplicationContext context)
        {
            var propertyName = nameof(BaseVersionedEntity.RowVersion);

            foreach (EntityEntry entry in context.ChangeTracker.Entries())
            {
                var isAllowedState = entry.State is EntityState.Modified or EntityState.Deleted;
                bool isVersionedEntity = entry.Entity is BaseVersionedEntity
                    || entry.Properties.Any(x => x.Metadata.Name.Equals(propertyName));

                if (isVersionedEntity && isAllowedState)
                {
                    entry.Property(propertyName).OriginalValue = entry.Property(propertyName).CurrentValue;
                }
            }
        }
    }
}
