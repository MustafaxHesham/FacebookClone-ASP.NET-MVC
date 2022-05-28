using FacebookClone.Data;
using FacebookClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacebookClone.Controllers
{
    public class CommentController : Controller
    {
        private AppDbContext context = new AppDbContext();
        public string AddComment(string CommentContent, string pid)
        {
            int PID = Int32.Parse(pid);
            int Uid = (int)Session["ID"];
            Post post = context.Posts.FirstOrDefault(p => p.Id == PID);
            User user = context.Users.FirstOrDefault(p => p.Id == Uid);
            var result = context.Comments.Add(new Models.Comment { CommentContent = CommentContent,
                Post = post, User = user
            });
            context.SaveChanges();
            return (result != null) ? "Done" : "Not Done";
        }
    }
}