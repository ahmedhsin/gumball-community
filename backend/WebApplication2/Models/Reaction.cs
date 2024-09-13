using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SocialMediaApp.Enums;
using SocialMediaApp.Models.SocialMediaApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public required int AuthorId { get; set; }
        public  int? PostId { get; set; }
        public int? CommentId { get; set; }

        public required DateTime CreatedAt { get; set; }
        public required ReactionType React { get; set; }

       
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }

    }
}
