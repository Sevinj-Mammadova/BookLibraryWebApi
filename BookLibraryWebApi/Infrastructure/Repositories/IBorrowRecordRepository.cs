using BookLibraryWebApi.Domain.Entities;

namespace BookLibraryWebApi.Infrastructure.Repositories
{
    public interface IBorrowRecordRepository
    {
        Task AddBorrowRecordAsync(BorrowRecord borrowRecord);
        Task<BorrowRecord?> GetActiveBorrowRecordAsync(int bookId, int userId);
        Task UpdateBorrowRecordAsync(BorrowRecord borrowRecord);

    }
}
