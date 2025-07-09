using System.Data.Common;
using BookLibraryWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookLibraryWebApi.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BorrowRecord>()
                .Property(b => b.BorrowDate)
                .HasDefaultValueSql("CAST(GETDATE() AS DATE)");
            modelBuilder.Entity<BorrowRecord>()
                .Property(b => b.DueDate)
                .HasDefaultValueSql("DATEADD(DAY, 14, CAST(GETDATE() AS DATE))");
            modelBuilder.Entity<Book>()
                .Property(b => b.IsAvailable)
                .HasDefaultValue(true);
        }
    }
    
}
