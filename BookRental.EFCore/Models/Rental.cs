using System.ComponentModel.DataAnnotations;

namespace BookRentalAPI.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsOverdue { get; set; }

        public Book Book { get; set; }

        public User User { get; set; }

    }
}
