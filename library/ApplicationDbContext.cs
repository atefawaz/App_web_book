using Microsoft.EntityFrameworkCore;

namespace App_web_book.library
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Define DbSets for your entities
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; } // Added DbSet for User
    }
}
