using FacebookClone.Data;
using FacebookClone.Models;
using FacebookClone.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacebookClone.Controllers
{
    public class PostController : Controller
    {
        public AppDbContext context = new AppDbContext();

        [HttpPost]
        public string AddNewPost(string postContent)
        {
            Post post = new Post();
            int Uid = (int)Session["ID"];
            post.PostContent = postContent;
            post.Publisher = context.Users.FirstOrDefault(u => u.Id == Uid);
            int result = context.Posts.Add(post).Id;
            context.SaveChanges();
            int Pid = context.Posts.Max(p => p.Id);
            return Pid.ToString();
        }

        public ActionResult RestrictedUsers(int? id)
        {
            ViewBag.pid = id;
            int myId = (int)Session["ID"];
            if (Session["ID"] == null)
                return View("Login");
            List<Friendship> friendsCollection = context.Friendships.Include("User1").Include("User2").Where(fs => (fs.User1ID == myId || fs.User2ID == myId) && fs.IsFriend == true).ToList();
            List<RestrictedUserViewModel> rUsers = new List<RestrictedUserViewModel>();
            foreach (var friend in friendsCollection)
            {
                if (friend.User1ID == myId)
                {
                    var innerResult = context.RestrictedUsers.FirstOrDefault(u => u.PostID == id && u.UserID == friend.User2ID);
                    if (innerResult == null)
                        rUsers.Add(new RestrictedUserViewModel { IsRestricted = false, user = friend.User2 });
                    else
                        rUsers.Add(new RestrictedUserViewModel { IsRestricted = true, user = friend.User2 });
                    continue;
                }
                var result = context.RestrictedUsers.FirstOrDefault(u => u.PostID == id && u.UserID == friend.User1ID);
                if (result == null)
                    rUsers.Add(new RestrictedUserViewModel { IsRestricted = false, user = friend.User1 });
                else
                    rUsers.Add(new RestrictedUserViewModel { IsRestricted = true, user = friend.User1 });
            }
            return View(rUsers);
        }

        public string Restrictuser(int uid, int pid)
        {
            var rUser = context.RestrictedUsers.FirstOrDefault(u => u.UserID == uid && u.PostID == pid);
            if (rUser == null)
            {
                context.RestrictedUsers.Add(new RestrictedUser { UserID = uid, PostID = pid });
                context.SaveChanges();
                return "1";
            }
            context.RestrictedUsers.Remove(rUser);
            context.SaveChanges();
            return "0";
        }
    }
}