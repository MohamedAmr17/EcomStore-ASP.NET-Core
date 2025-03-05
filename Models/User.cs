using System.ComponentModel.DataAnnotations;


namespace Ecom.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

    }
}