using BookLibraryWebApi.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookLibraryWebApi.Application.DTOs
{
    public class BorrowRecordDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        //public Book Book { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; } 
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned => ReturnDate.HasValue;

    }
}
