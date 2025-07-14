using System.ComponentModel.DataAnnotations;

namespace BookLibraryWebApi.Domain.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        //[Required]
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public bool IsAvailable { get; set; } = true;


    }
}
