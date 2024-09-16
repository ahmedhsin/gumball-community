namespace SocialMediaApp.Models;

public class AuthorFollow
{
    public int Id { get; set; }
    public int FollowerId { get; set; }
    public int FollowingId { get; set; }
    
    public Author Follower { get; set; }
    public Author Following { get; set; }
}