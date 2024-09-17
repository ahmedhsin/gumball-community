using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;

namespace SocialMediaApp
{
	public class SocialMediaContext : DbContext
	{
		public DbSet<Author> Authors { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		
		public DbSet<React> Reactions { get; set; }
		
		public DbSet<AuthorFollow> AuthorFollows { get; set; }

		public SocialMediaContext(DbContextOptions<SocialMediaContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Comment>()
			 .HasOne(c => c.ParentComment)
			 .WithMany(c => c.ChildComments)
			 .HasForeignKey(c => c.ParentId)
			 .OnDelete(DeleteBehavior.Cascade);
			
			modelBuilder.Entity<AuthorFollow>()
				.HasOne<Author>(af => af.Follower)
				.WithMany(a => a.Followers)
				.HasForeignKey(af => af.FollowerId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<AuthorFollow>()
				.HasOne<Author>(af => af.Following)
				.WithMany(a => a.Followings)
				.HasForeignKey(af => af.FollowingId)
				.OnDelete(DeleteBehavior.Restrict); 
			
			
			base.OnModelCreating(modelBuilder);
			
			

		}
	}
}
