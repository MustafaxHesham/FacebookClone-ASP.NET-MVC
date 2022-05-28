using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FacebookClone.Models
{
    public class RestrictedUser
    {
        [Key]
        [Column(Order = 1)]
        public int PostID { get; set; }
        public Post Post { get; set; }
        [Key]
        [Column(Order = 2)]
        public int UserID { get; set; }
        public User User { get; set; }

    }
}