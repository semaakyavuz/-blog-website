using System.ComponentModel.DataAnnotations;

namespace _blog_website.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        public string? Username { get; set; } // ✅ EKLENDİ

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public string Role { get; set; } = "User";
    }
}
