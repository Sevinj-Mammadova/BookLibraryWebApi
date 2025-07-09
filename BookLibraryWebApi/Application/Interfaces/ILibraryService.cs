using BookLibraryWebApi.Domain.Entities;

namespace BookLibraryWebApi.Application.Interfaces
{
    public interface ILibraryService
    {
        Task<BorrowRecord> BorrowBookAsync(int bookId, int userId);
        Task<bool> ReturnBookAsync(int bookId, int userId);
    }
}
