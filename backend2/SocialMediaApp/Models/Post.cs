using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SocialMediaApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public string? React { get; set; }
        public int AuthorId { get; set; }
        public string? ImageUrl { get; set; }
        public Author Author { get; set; }
        public List<Comment> Comments { get; set; }
        public List<React> Reactions { get; set; }
        

    }

}
