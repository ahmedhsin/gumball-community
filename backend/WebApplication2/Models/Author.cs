namespace SocialMediaApp.Models
{
    namespace SocialMediaApp.Models
    {
        public class Author
        {
            public int Id { get; set; }
            public required string UserName { get; set; }
            public required string Email { get; set; }
            public required string Password { get; set; }
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Bio { get; set; }
            public string? LivesIn { get; set; }
            public DateOnly? DateOFBirth { get; set; }
            public string? Gender { get; set; }

            public ICollection<Post> Posts { get; set; }
            public ICollection<Reaction> Reactions { get; set; }
            public ICollection<Comment> Comments { get; set; }

            public ICollection<AuthorFollow> FollowingAuthors { get; set; }
            public ICollection<AuthorFollow> FollowerAuthors { get; set; }


        }
    }
}
