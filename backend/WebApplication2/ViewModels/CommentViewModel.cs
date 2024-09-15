namespace SocialMediaApp.ViewModels
{
    public class CommentViewModel
    {
       public int Id { get; set; }
        public int AuthorId { get; set; } 
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public int? PostId { get; set; }
        public int? ParentCommentId { get; set; }
        public string? AuthorProfileImage { get; set; }
       
        public string? HumanizedDate { get; set; }
        public ICollection<CommentViewModel>? SubComments { get; set; } = new List<CommentViewModel>();
     //   public bool? ShowSubComments { get; set; } = false;
       // public bool? ShowReplyForm { get; set; } = false;
    }
}
