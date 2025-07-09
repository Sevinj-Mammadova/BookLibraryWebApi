using System.ComponentModel.DataAnnotations;

namespace BookLibraryWebApi.Domain.Entities
{
    public class BorrowRecord
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; } = DateTime.UtcNow.Date;
        public DateTime DueDate { get; set; } = DateTime.UtcNow.Date.AddDays(14);
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned => ReturnDate.HasValue;
    }
}
