using SocialMediaApp.Models.SocialMediaApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public required int AuthorId { get; set; }
        public  int? PostId { get; set; }
        public int? ParentCommentId { get; set; }
        public  required string Content { get; set; }
        public required DateTime CreatedAt { get; set; }


        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        [ForeignKey("ParentCommentId")]
        public Comment ParentComment { get; set; }

        public ICollection<Comment> SubComments { get; set; }

        [ForeignKey("PostId")]
        public  Post Post { get; set; }

        public ICollection<Reaction> Reactions { get; set; }
    }
}
