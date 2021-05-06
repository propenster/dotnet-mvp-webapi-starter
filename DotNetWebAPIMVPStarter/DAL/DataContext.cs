using DotNetWebAPIMVPStarter.Models;
using DotNetWebAPIMVPStarter.Models.Blog;
using DotNetWebAPIMVPStarter.Models.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.DAL
{
    public class DataContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<StripeCustomer> StripeCustomers { get; set; }
        //block 
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            //One to Many Relationship =>  A Product can have a billion reviews if there are any?
            Builder.Entity<Product>().HasMany<Review>().WithOne().HasForeignKey(p => p.ProductId);
            Builder.Entity<User>().HasOne<StripeCustomer>().WithOne();
            Builder.Entity<User>().HasMany<Post>().WithOne().HasForeignKey(prop => prop.AuthorId);
            Builder.Entity<Post>().HasMany<Comment>().WithOne().HasForeignKey(prop => prop.PostId);
            Builder.Entity<Category>().HasMany<Post>().WithOne();
        }
    }
}
