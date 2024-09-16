namespace SocialMediaApp.ViewModels;

public class CreateAuthorViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IFormFile? ImageFile { get; set; } 

}