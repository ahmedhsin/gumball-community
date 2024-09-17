using SocialMediaApp.Models;

namespace SocialMediaApp.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string React { get; set; }
        public string CreatedAt { get; set; }
        public AuthorViewModel? Author { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        
        public Dictionary<int, int> Reactions { get; set; }
        public string ImageUrl { get; set; }
    }
}
