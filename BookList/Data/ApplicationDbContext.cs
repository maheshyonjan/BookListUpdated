using BookList.Models;
using Microsoft.EntityFrameworkCore;

namespace BookList.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MAHESH-ACER-ASP\\SQLEXPRESS;Database=BookListDb;Trusted_Connection=True;TrustServerCertificate=True", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Book> Books{ get; set; }
    }
}
