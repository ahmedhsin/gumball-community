using SocialMediaApp.Models.SocialMediaApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public  required int AuthorId { get; set; }
        public required string Content { get; set; }
        public int? MyReaction { get; set; }

        public required DateTime CreatedAt { get; set; }
        public string? MediaPath { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        public ICollection<Reaction>? Reactions { get; set; } = new List<Reaction>();
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();


    }
}
