using System.Data.Common;
using BookLibraryWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryWebApi.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
    }
}
