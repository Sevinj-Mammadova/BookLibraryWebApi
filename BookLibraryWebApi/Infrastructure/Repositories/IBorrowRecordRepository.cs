using BookLibraryWebApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryWebApi.Infrastructure.Repositories
{
    public interface IBorrowRecordRepository
    {
        Task AddBorrowRecordAsync(BorrowRecord borrowRecord);
        Task<BorrowRecord?> GetActiveBorrowRecordAsync(int bookId, int userId);
        Task UpdateBorrowRecordAsync(BorrowRecord borrowRecord);
        Task<List<BorrowRecord>> GetAllBorrowRecordsAsync();
        Task<List<BorrowRecord>> GetBorrowRecordsByUserIdAsync(int userId);


    }
}
