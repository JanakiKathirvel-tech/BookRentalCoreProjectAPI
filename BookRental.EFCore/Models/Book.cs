using System.ComponentModel.DataAnnotations;

namespace BookRentalAPI.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Author { get; set; }

        [Required]
        [MaxLength(100)]
        public string ISDN { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Rental> Rentals { get; } = new List<Rental>();
    }
}
