using static System.Net.Mime.MediaTypeNames;

namespace SocialMediaApp.Models
{

	public class Comment
	{
		public int Id { get; set; }
		
		public int? ParentId { get; set; }
		public Comment ParentComment { get; set; }
		public DateTime CreatedAt { get; set; }

		public string Content { get; set; }
		public int PostId { get; set; }
		public Post Post { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<Comment> ChildComments { get; set; }

    }
}
