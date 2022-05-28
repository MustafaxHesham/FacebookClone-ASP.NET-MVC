using FacebookClone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace FacebookClone.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("FbDB")
        {

        }
        public DbSet<User> Users { set; get; }
        public DbSet<Friendship> Friendships { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<React> Reacts { set; get; }
        public DbSet<RestrictedUser> RestrictedUsers { set; get; }
    }
}