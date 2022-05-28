using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FacebookClone.Models;

namespace FacebookClone.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string PostContent { get; set; }
        public User Publisher { get; set; }
        public List<React> Reacts { get; set; }
        public List<RestrictedUser> RestrictedUsers { get; set; }
        public List<Comment> Comments { get; set; }
    }
}