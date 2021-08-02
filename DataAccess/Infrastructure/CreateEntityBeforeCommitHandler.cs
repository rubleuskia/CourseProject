using System;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Infrastructure
{
    public class CreateEntityBeforeCommitHandler : BaseEntityBeforeCommitHandler
    {
        public CreateEntityBeforeCommitHandler(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        protected override bool ShouldUpdate(EntityEntry entry)
        {
            return entry.State == EntityState.Added;
        }

        protected override void UpdateEntity(BaseEntity entity, string userId, DateTime now)
        {
            entity.CreatedAt = now;
            entity.UpdatedAt = now;
            entity.CreatedById = userId;
            entity.UpdatedById = userId;
        }
    }
}
