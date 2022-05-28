using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FacebookClone.Models
{
    public class React
    {
        [Key]
        [Column(Order = 1)]
        public int PostId { get; set; }
        public Post Post { get; set; }
        [Key]
        [Column(Order = 2)]
        public int UserID { get; set; }
        public User User { get; set; }
        public bool Like { get; set; }
    }
}