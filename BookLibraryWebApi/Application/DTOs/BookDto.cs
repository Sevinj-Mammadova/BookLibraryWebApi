using System.ComponentModel.DataAnnotations;

namespace BookLibraryWebApi.Application.DTOs
{
    public class BookDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        //[Required]
        public string Description { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        
    }
}
