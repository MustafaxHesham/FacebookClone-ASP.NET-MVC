using FacebookClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookClone.ViewModel
{
    public class RestrictedUserViewModel
    {
        public bool IsRestricted { get; set; }
        public User user { get; set; }
    }
}