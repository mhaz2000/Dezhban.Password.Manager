using Dezhban.ApplicationServices.Services.Users;
using Dezhban.Infrastructure;
using Dezhban.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dezhban.ApplicationServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, string dbPath)
        {
            services.AddInfrastructure(dbPath);

            services.AddScoped<IUserService, UserService>();

            // Register application services here if needed in the future
            return services;
        }

        public static void EnsureDatabaseMigrated(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }
    }
}
