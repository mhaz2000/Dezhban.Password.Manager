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
            modelBuilder.Entity<PasswordModel>(entity =>
            {
                entity.Property(e => e.AdditionalData)
                      .HasConversion(
                          v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                          v => string.IsNullOrEmpty(v)
                              ? new Dictionary<string, string>()
                              : JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions)null)
                      );
            });
        }
    }
}
