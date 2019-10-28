using System;
using Biosis.Model;
using Microsoft.EntityFrameworkCore;

namespace Biosis.Model
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Research> Research { get; set; }
        public DbSet<TransData> TransData { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("CORE");
            modelBuilder.Entity<TransData>();
            modelBuilder.Entity<User>().HasMany(u => u.Researches).WithOne(r => r.User).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Research>().HasMany(r => r.TransData).WithOne(td => td.Research).HasForeignKey(td => td.ResearchId).OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("host=localhost;port=5433;database=Biosis;user id=postgres;password=postgres;Command Timeout=60");
        }
    }
}