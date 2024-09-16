namespace SocialMediaApp.ViewModels
{
    public class ReactViewModel
    {
        public int Id { get; set; }
        public int React { get; set; } 
        public int PostId { get; set; }
        
        public AuthorViewModel Author { get; set; }
    }
}