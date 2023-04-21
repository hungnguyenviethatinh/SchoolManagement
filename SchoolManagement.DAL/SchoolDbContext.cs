using Microsoft.EntityFrameworkCore;
using SchoolManagement.DAL.Models;

namespace SchoolManagement.DAL
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public SchoolDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().Property(s => s.Name).HasMaxLength(30);
            modelBuilder.Entity<Teacher>().Property(t => t.Name).HasMaxLength(30);
        }
    }
}
