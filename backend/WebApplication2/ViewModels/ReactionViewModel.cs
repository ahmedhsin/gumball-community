namespace SocialMediaApp.ViewModels
{
    public class ReactionViewModel
    {
        public required int AuthorId { get; set; }
        public int PostId { get; set; }

        // public required DateTime CreatedAt { get; set; }
        public required int ReactionType { get; set; }
    }
}
