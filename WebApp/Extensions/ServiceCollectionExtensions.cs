using DataAccess;
using DataAccess.Entities;
using DataAccess.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void EnableRuntimeCompilation(this IServiceCollection services, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                services.AddRazorPages().AddRazorRuntimeCompilation();
            }
        }

        public static void RegisterEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "MyPass@word";
            builder.InitialCatalog = "WebAppDatabase";

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(new SqlConnection(builder.ConnectionString)));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>();

            services.AddTransient<IBeforeCommitHandler, CreateEntityBeforeCommitHandler>();
            services.AddTransient<IBeforeCommitHandler, UpdateEntityBeforeCommitHandler>();
            services.AddTransient<IBeforeCommitHandler, VersionedEntityBeforeCommitHandler>();
        }
    }
}
