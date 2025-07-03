namespace BookLibraryWebApi.Application.Interfaces
{
    public interface ILibraryService
    {
        Task<bool> BorrowBookAsync(int bookId, int userId);
    }
}
