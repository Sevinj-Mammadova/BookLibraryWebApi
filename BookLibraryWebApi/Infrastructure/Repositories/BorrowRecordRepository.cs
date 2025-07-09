using BookLibraryWebApi.Domain.Entities;
using BookLibraryWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryWebApi.Infrastructure.Repositories
{
    public class BorrowRecordRepository : IBorrowRecordRepository
    {
        private readonly DataContext _dataContext;

        public BorrowRecordRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddBorrowRecordAsync(BorrowRecord borrowRecord)
        {
            await _dataContext.BorrowRecords.AddAsync(borrowRecord);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<BorrowRecord?> GetActiveBorrowRecordAsync(int bookId, int userId)
        {
            return await _dataContext.BorrowRecords
                .FirstOrDefaultAsync(x=> x.BookId == bookId && x.UserId == userId && x.ReturnDate == null);   
        }
        public async Task UpdateBorrowRecordAsync(BorrowRecord borrowRecord)
        {
            _dataContext.BorrowRecords.Update(borrowRecord);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<List<BorrowRecord>> GetAllBorrowRecordsAsync()
        {
            return await _dataContext.BorrowRecords.ToListAsync();
        }
    }
}
