using FacebookClone.Data;
using FacebookClone.Models;
using FacebookClone.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacebookClone.Controllers
{
    public class HomeController : Controller
    {


        [HttpGet]
        private List<Post> GetAllPosts()
        {
            int Uid = (int)Session["ID"];
            var result = context.Friendships.Include("User1").Include("User1.Posts").Include("User1.Posts.Comments").Include("User1.Posts.Comments.User").Include("User1.Posts.Reacts").Include("User2").Include("User2.Posts").Include("User2.Posts.Comments").Include("User2.Posts.Comments.User").Include("User2.Posts.Reacts").Where(fs => (fs.User1ID == Uid || fs.User2ID == Uid) && fs.IsFriend == true).ToList();
            List<Post> posts = new List<Post>();
            if (result.Count > 0)
            {
                foreach (Friendship friendship in result)
                {
                    if (friendship.User1ID == Uid)
                        posts = posts.Concat(friendship.User2.Posts).ToList();
                    else
                        posts = posts.Concat(friendship.User1.Posts).ToList();
                }
            }
            List<RestrictedUser> ru = context.RestrictedUsers.ToList();
            foreach (var restrict in ru)
                posts.RemoveAll(p => p.Id == restrict.PostID && Uid == restrict.UserID);
            return posts;
        }


        public AppDbContext context = new AppDbContext();
        // GET: Home
        public ActionResult Index() => RedirectToAction("Login", "User");

        public ActionResult Timeline() 
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "User");

            int uid = (int)Session["ID"];
            TimelineViewModel tvm = new TimelineViewModel()
                { loggedInUser = context.Users.FirstOrDefault(u => u.Id == uid) ,
                  Posts = GetAllPosts()};
            return View(tvm);
        }

        public ActionResult Search() => View();

        [HttpGet]
        public JsonResult GetUsers(string EmailOrPhoneNumber)
        {
            return Json(context.Users.Where(u => u.Email == EmailOrPhoneNumber || u.PhoneNumber == EmailOrPhoneNumber), behavior: JsonRequestBehavior.AllowGet);
        }

    }
}