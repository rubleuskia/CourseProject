using System;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Infrastructure
{
    public class UpdateEntityBeforeCommitHandler : BaseEntityBeforeCommitHandler
    {
        public UpdateEntityBeforeCommitHandler(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        protected override bool ShouldUpdate(EntityEntry entry)
        {
            return entry.State == EntityState.Modified;
        }

        protected override void UpdateEntity(BaseEntity entity, string userId, DateTime now)
        {
            entity.UpdatedAt = now;
            entity.UpdatedById = userId;
        }
    }
}
