using Microsoft.EntityFrameworkCore;
using _blog_website.Models;

namespace _blog_website.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }


        // Admin kullanıcıyı ekliyoruz
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 999, // Benzersiz ID
                FullName = "Admin Kullanıcı",
                Email = "admin@test.com",
                Password = "1234",
                Username = "admin",  // Güvenlik için sonra hash'leyeceğiz
                Role = "Admin"
            });
        }
    }
}
