namespace SocialMediaApp.ViewModels
{
    public class CommentViewModel
    {

        public int Id { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; }
        public AuthorViewModel Author { get; set; }
        public ICollection<CommentViewModel> SubComments { get; set; }

    }
}
