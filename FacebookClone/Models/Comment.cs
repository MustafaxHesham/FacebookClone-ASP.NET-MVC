using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FacebookClone.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
        public string CommentContent { get; set; }
    }
}