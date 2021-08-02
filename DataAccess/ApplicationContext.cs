using System;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        private readonly IServiceProvider _serviceProvider;

        public DbSet<Account> Accounts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IServiceProvider serviceProvider)
            : base(options)
        {
            _serviceProvider = serviceProvider;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigureAccount(builder);
            CreateSeededUser(builder);
        }

        private static void ConfigureAccount(ModelBuilder builder)
        {
            builder.Entity<Account>()
                .Property(x => x.RowVersion)
                .IsRowVersion();

            builder.Entity<User>()
                .Property(x => x.RowVersion)
                .IsRowVersion();

            builder.Entity<Account>()
                .HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Account>()
                .HasOne(x => x.UpdatedBy)
                .WithMany()
                .HasForeignKey(x => x.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            RunBeforeCommitHandlers();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            throw new NotSupportedException();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new NotSupportedException();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            RunBeforeCommitHandlers();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void RunBeforeCommitHandlers()
        {
            foreach (var commitHandler in _serviceProvider.GetServices<IBeforeCommitHandler>())
            {
                commitHandler.Execute(this);
            }
        }

        private static void CreateSeededUser(ModelBuilder builder)
        {
            var role = new IdentityRole
            {
                Id = "5b9ef978-d85b-4050-b636-c0f4b4f4f708",
                Name = "admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "9906c2f4-4941-4f1e-ae6e-6b67258c526f",
            };

            var user = new User
            {
                Id = "bdb94046-f01d-4080-a989-341c3e88ed50",
                Email = "admin@admin.com",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Age = 100,
                PasswordHash = "AQAAAAEAACcQAAAAEOlRAst5kneREHoKVJsYzh3MhWbA3Z9lJl2aw/Hk4DO9C9gtXT/CiI8Q7ND1arCpQA==",
                ConcurrencyStamp = "c568026f-8944-41b6-8fcc-84c613158e27",
                SecurityStamp = "7b8588f2-6429-4d36-9097-dcc81abdf4a7",
            };

            builder.Entity<User>().HasData(user);
            builder.Entity<IdentityRole>().HasData(role);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "5b9ef978-d85b-4050-b636-c0f4b4f4f708",
                UserId = "bdb94046-f01d-4080-a989-341c3e88ed50",
            });
        }
    }
}
