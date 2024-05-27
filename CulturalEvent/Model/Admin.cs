using System.ComponentModel.DataAnnotations;

namespace CulturalEvent.Model
{
    public class Admin
    {
        [Key]
        [Required]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Admin name is required.")]
        [StringLength(50, ErrorMessage = "Admin name cannot be longer than 50 characters.")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "Email ID is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
