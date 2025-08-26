using Dezhban.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Dezhban.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<PasswordModel> Passwords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configs
        }
    }
}
