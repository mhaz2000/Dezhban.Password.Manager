using Dezhban.Core.Repositories;
using Dezhban.Infrastructure.Data;
using Dezhban.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dezhban.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbPath)
        {
            // SQLite EF Core DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite($"Filename={dbPath}");
            });

            // Register repositories (if any)
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordRepository, PasswordRepository>();

            return services;
        }

    }
}
