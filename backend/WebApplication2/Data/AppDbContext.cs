using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.Models.SocialMediaApp.Models;

namespace SocialMediaApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }
       // public DbSet<AuthorFollow> AuthorFollows { get; set; }
        public DbSet<Comment> Comments { get; set; } // Add DbSet for the join entity
        public DbSet<Reaction> Reactions { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
           
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Author,Post (1-->m)
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);



            // Author,Comment (1-->m)
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Comments)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);


            // Author,React (1-->m)
            modelBuilder.Entity<Author>()
               .HasMany(a => a.Reactions)
               .WithOne(r => r.Author)
               .HasForeignKey(r => r.AuthorId)
               .OnDelete(DeleteBehavior.NoAction); 

            // Post,React (1-->m)
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Reactions)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.NoAction);



            // Post,Comment  1--->m
            modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);



            // Comment, subcomments (self Realationship) (1--->m)
            modelBuilder.Entity<Comment>()
            .HasMany(c => c.SubComments) 
            .WithOne(c => c.ParentComment) 
            .HasForeignKey(c => c.ParentCommentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);


            // Follewer<---->>Followed (m--->n)


            // author have many followers
            modelBuilder.Entity<Author>()
         .HasMany(a => a.FollowerAuthors) // Each Author can have many AuthorFollows as followers
         .WithOne(af => af.Following)    // Each AuthorFollow has one Author being followed
         .HasForeignKey(af => af.FollowingId) // Foreign key to Author being followed
         .OnDelete(DeleteBehavior.Cascade); // Avoid cyclic cascade paths

            // Configure the relationship for following authors with cascade delete behavior
            modelBuilder.Entity<Author>()
                .HasMany(a => a.FollowingAuthors) // Each Author can follow many Authors
                .WithOne(af => af.Follower)       // Each AuthorFollow has one Author who is following
                .HasForeignKey(af => af.FollowerId) // Foreign key to Author who is following
                .OnDelete(DeleteBehavior.Cascade);







        }


    }
}
