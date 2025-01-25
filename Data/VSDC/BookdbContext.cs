using App_web_book.Entities.VSDCEntities;
using Microsoft.EntityFrameworkCore;

namespace App_web_book.Data.VSDC
{
    public partial class BookdbContext : DbContext
    {
        public BookdbContext()
        {
        }

        public BookdbContext(DbContextOptions<BookdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Fallback to a hardcoded connection string only if not configured via DI.
                optionsBuilder.UseSqlServer("Server=tcp:atef12.database.windows.net,1433;Initial Catalog=bookdb;Persist Security Info=False;User ID=atef;Password=test2004$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly configure the Book entity
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id); // Primary key
                entity.Property(b => b.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(b => b.Author)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(b => b.Genre)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(b => b.PublicationDate)
                    .IsRequired();
            });

            // Allow partial method for further configuration
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}