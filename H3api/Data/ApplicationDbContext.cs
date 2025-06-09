using H3api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace H3api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
    .HasOne(b => b.Cover)
    .WithOne(c => c.Book)
    .HasForeignKey<Cover>(c => c.BookId)
    .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
