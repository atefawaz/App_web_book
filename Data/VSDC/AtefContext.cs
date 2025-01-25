using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using App_web_book.Entities.VSDCEntities;
using App_web_book.Services;

namespace App_web_book.Data
{
    public partial class AtefContext : IdentityDbContext<IUserService>
    {
        public AtefContext()
        {
        }

        public AtefContext(DbContextOptions<AtefContext> options)
            : base(options)
        {
        }

        // DbSet for Book
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Fallback connection string
                optionsBuilder.UseSqlServer("Server=tcp:atef12.database.windows.net,1433;Initial Catalog=atef;Persist Security Info=False;User ID=atef;Password=test2004$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // For Identity schema

            // Additional model configurations for Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Name).IsRequired();
                entity.Property(b => b.Author).IsRequired();
                entity.Property(b => b.Genre).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
