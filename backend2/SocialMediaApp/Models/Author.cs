using static System.Net.Mime.MediaTypeNames;

namespace SocialMediaApp.Models
{
	public class Author
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string ProfileImageUrl { get; set; }
		public string Password { get; set; }
		public List<Post> Posts { get; set; } = new List<Post>();
		public List<Comment> Comments { get; set; } = new List<Comment>();
		public ICollection<AuthorFollow> Followers { get; set; }
		public ICollection<AuthorFollow> Followings { get; set; }
	}

}
