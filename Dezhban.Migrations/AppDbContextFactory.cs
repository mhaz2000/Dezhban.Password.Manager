using Dezhban.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dezhban.Migrations;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // ⚡ Use a local SQLite file for migrations
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\Mohammad\\AppData\\Local\\Packages\\com.companyname.dezhban.app_9zz4h110yvjzm\\LocalState\\PM_App.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}