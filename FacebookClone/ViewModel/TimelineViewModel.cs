using FacebookClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookClone.ViewModel
{
    public class TimelineViewModel
    {
        public User loggedInUser { get; set; }
        public List<Post> Posts { get; set; }
    }

    /*
     * Logic: Return timelinevm
     * 
     */
}