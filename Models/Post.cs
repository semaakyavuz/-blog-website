using System;
using System.ComponentModel.DataAnnotations;

namespace _blog_website.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Content { get; set; }

        public string? AuthorEmail { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
