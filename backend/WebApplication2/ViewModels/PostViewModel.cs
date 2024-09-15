using SocialMediaApp.Models.SocialMediaApp.Models;

namespace SocialMediaApp.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string? AuthorProfileImage { get; set; }
        public int? MyReaction {  get; set; } 
        public string Content { get; set; }
        public string ?HumanizedDate { get; set; }
        public string? MediaPath { get; set; }

        public Dictionary<int, int>? Reactions { get; set; } // ReactionType and count
        
        public ICollection<CommentViewModel>? Comments { get; set; } = new List<CommentViewModel>();
        public IFormFile? Photo { get; set; }
    }
}
