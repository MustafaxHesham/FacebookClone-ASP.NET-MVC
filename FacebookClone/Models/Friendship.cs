using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FacebookClone.Models
{
    public class Friendship
    {
        [Key]
        [Column(Order = 1)]
        public int User1ID { get; set; }
        public User User1 { get; set; }
        [Key]
        [Column(Order = 2)]
        public int User2ID { get; set; }
        public User User2 { get; set; }
        public bool? IsFriend { get; set; }
    }
}