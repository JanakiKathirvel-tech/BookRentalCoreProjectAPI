using System.ComponentModel.DataAnnotations;

namespace BookRentalAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string UserEmail { get; set; }
    }
}
