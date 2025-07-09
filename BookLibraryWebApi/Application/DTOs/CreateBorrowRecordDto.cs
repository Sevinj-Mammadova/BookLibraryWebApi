namespace BookLibraryWebApi.Application.DTOs
{
    public class CreateBorrowRecordDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
    }

}
