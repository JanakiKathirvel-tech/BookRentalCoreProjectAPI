using System.ComponentModel.DataAnnotations;

namespace BookRentalAPI.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(255)]
        public string GenreName { get; set; }


        public ICollection<Book> Books { get; } = new List<Book>();
    }
}
