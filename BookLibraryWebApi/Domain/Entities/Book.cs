using System.ComponentModel.DataAnnotations;

namespace BookLibraryWebApi.Domain.Entities
{
    public class Book
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
