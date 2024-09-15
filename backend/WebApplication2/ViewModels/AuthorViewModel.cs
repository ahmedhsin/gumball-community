namespace SocialMediaApp.ViewModels
{
    public class AuthorViewModel
    {
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
    }
}
