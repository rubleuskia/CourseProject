using System;
using System.Linq;
using System.Security.Claims;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Infrastructure
{
    public abstract class BaseEntityBeforeCommitHandler : IBeforeCommitHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseEntityBeforeCommitHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Execute(ApplicationContext context)
        {
            var userId = GetUserId();
            var now = DateTime.UtcNow;

            foreach (EntityEntry entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is not BaseEntity entity)
                {
                    continue;
                }

                if (ShouldUpdate(entry))
                {
                    UpdateEntity(entity, userId, now);
                }
            }
        }

        protected abstract void UpdateEntity(BaseEntity entity, string userId, DateTime now);
        protected abstract bool ShouldUpdate(EntityEntry entity);

        private string GetUserId()
        {
            if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated == true)
            {
                return _httpContextAccessor.HttpContext.User.Claims
                    .Single(x => x.Type == ClaimTypes.NameIdentifier)
                    .Value;
            }

            return Guid.Empty.ToString();
        }
    }
}
