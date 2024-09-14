using SocialMediaApp.Models.SocialMediaApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class AuthorFollow
    {
        public int Id { get; set; }
        public required DateTime CreatedAt { get; set; }

        public int? FollowerId { get; set; }
        public int? FollowingId { get; set; }

        [ForeignKey("FollowerId")]
        public Author Follower { get; set; }

        [ForeignKey("FollowingId")]
        public Author Following { get; set; }



    }
}
