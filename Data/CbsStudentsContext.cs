#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using cbsStudents.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace cbsStudents.Data;

    public class CbsStudentsContext : IdentityDbContext
    {
        public CbsStudentsContext (DbContextOptions<CbsStudentsContext> options)
            : base(options)
        {
        }


        /* protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedPosts(builder);
            this.SeedComments(builder);
        }

         */
        public DbSet<cbsStudents.Models.Entities.Post> Post { get; set; }

        public DbSet<cbsStudents.Models.Entities.Comment> Comment { get; set; }

       /*  private void SeedPosts(ModelBuilder builder)
        {
            builder.Entity<Post>().HasData(
                new Post() { Id = 1, Created = DateTime.Now, Text = "This is post 1", Title = "Post no 1", Status = PostStatus.DRAFT },
                new Post() { Id = 2, Created = DateTime.Now, Text = "This is post 2", Title = "Post no 2", Status = PostStatus.DRAFT },
                new Post() { Id = 3, Created = DateTime.Now, Text = "This is post 3", Title = "Post no 3", Status = PostStatus.DRAFT }
            );
        }

        private void SeedComments(ModelBuilder builder)
        {
            builder.Entity<Comment>().HasData(
                new Comment() { CommentId = 1, Text = "Hello", TimeStamp = DateTime.Now, PostId = 1 },
                new Comment() { CommentId = 2, Text = "Hello again", TimeStamp = DateTime.Now, PostId = 1 },
                new Comment() { CommentId = 3, Text = "Hi", TimeStamp = DateTime.Now, PostId = 2 },
                new Comment() { CommentId = 4, Text = "Bye", TimeStamp = DateTime.Now, PostId = 3 }
            );
        } */
    }
