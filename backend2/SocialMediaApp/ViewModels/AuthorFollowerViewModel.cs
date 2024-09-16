using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Models
{
    public class AuthorFollowViewModel
    {

        public ICollection<Author> Authors { get; set; }
    }
}