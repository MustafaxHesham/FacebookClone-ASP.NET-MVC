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
    public class UserController : Controller
    {
        private AppDbContext context = new AppDbContext();

        [HttpGet]
        public ActionResult Login() => View();


        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("ID");
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel LVM)
        {
            var Result = context.Users.FirstOrDefault(u => u.Email == LVM.Email &&
            u.Password == LVM.Password);
            if (Result == null)
            {
                ViewBag.Invalid = "Invalid login attempt.";
                return View(LVM);
            }
            ViewBag.result = context.Friendships.Count(fs => fs.User2ID == Result.Id
            && fs.IsFriend == null);
            Session.Add("ID", Result.Id);
            return RedirectToAction("Timeline", "Home");
        }

        [HttpGet]
        public ActionResult Register() => View();

        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,FirstName,LastName,ProfileImageURL,City,Country,Email,Password,PhoneNumber")] User user)
        {
            if (ModelState.IsValid)
            {
                user.ProfileImageURL = "~/Images/ProfileImages/default.jpg";
                var result = context.Users.Add(user);
                context.SaveChanges();
                Session.Add("ID", result.Id);
                return RedirectToAction("EditProfile", "User");
            }
            return View();
        }
        


        //Validate Email Using AJAX
        [HttpGet]
        public string ValidateRegistration(string Email)
        => context.Users.FirstOrDefault(u => u.Email == Email) == null ? "valid" : "Invalid";


        //Validate Email Using AJAX
        [HttpGet]
        public string ValidateEmailEdition(string Email, int id)
        => context.Users.FirstOrDefault(u => u.Email == Email && id != u.Id) == null ? "valid" : "Invalid";


        public ActionResult MyProfile()
        {
            if (Session["ID"] == null)
                return View("Login");
            int uid = (int)Session["ID"];
            ViewBag.uid = uid;
            ViewBag.Posts = context.Posts.Count(p => p.Publisher.Id == uid);
            ViewBag.Friends = context.Friendships.Count(fs => (fs.User2ID == uid || fs.User1ID == uid) && fs.IsFriend == true);
            return View(context.Users.FirstOrDefault(u => u.Id == uid));
        }

        public ActionResult Details(int? id)
        {
            if (Session["ID"] == null)
                return View("Login");
            //State The User Friendship [NotFriend-Friend-RequestWasSent]
            ViewBag.uid = (int)Session["ID"];
            int uid = (int)Session["ID"];
            ViewBag.Posts = context.Posts.Count(p => p.Publisher.Id == id);
            ViewBag.Friends = context.Friendships.Count( fs => (fs.User2ID == id || fs.User1ID == id) && fs.IsFriend == true);

            Friendship friendshipState = context.Friendships.FirstOrDefault(fs => (fs.User1ID == uid && fs.User2ID == id) || (fs.User1ID == id && fs.User2ID == uid));
            if (friendshipState != null)
            {
                switch(friendshipState.IsFriend)
                {
                    case true:
                        ViewBag.isFriend = "Friend";
                        break;
                    case null:
                        ViewBag.isFriend = "Request on hold";
                        break;
                }
            }
            else
                ViewBag.isFriend = "Not Friends";
            return (id != null) ? View(context.Users.FirstOrDefault(u => u.Id == id)) : (ActionResult)HttpNotFound();
        }

        public ActionResult Friends(int? id)
        {
            ViewBag.uid = id;
            if (Session["ID"] == null)
                return View("Login");
            return View(context.Friendships.Include("User1").Include("User2").Where(fs => (fs.User1ID == id || fs.User2ID == id) && fs.IsFriend == true));
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Session["ID"] == null)
                return View("Login");
            ViewBag.Uid = Session["ID"];
            int uid = (int)Session["ID"];
            return View(context.Users.FirstOrDefault(u => u.Id == uid));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "Id,FirstName,LastName,ProfileImageURL,City,Country,Email,Password,PhoneNumber")] User user, HttpPostedFileBase ProfImage)
        {
            if (ProfImage != null)
            {
                string path = Server.MapPath("~/Images/ProfileImages");
                string ImageName = Guid.NewGuid().ToString() + Path.GetFileName(ProfImage.FileName);
                string FullPath = Path.Combine(path, ImageName);
                ProfImage.SaveAs(FullPath);
                user.ProfileImageURL = "~/Images/ProfileImages/" + ImageName;
            }
            var ExistedUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
            ExistedUser.FirstName = user.FirstName;
            ExistedUser.LastName = user.LastName;
            ExistedUser.Email = user.Email;
            ExistedUser.Country = user.Country;
            ExistedUser.City = user.City;
            ExistedUser.PhoneNumber = user.PhoneNumber;
            ExistedUser.Password = user.Password;
            if (user.ProfileImageURL != null && ExistedUser.ProfileImageURL != "~/Images/ProfileImages/default.jpg")
                System.IO.File.Delete(Server.MapPath(ExistedUser.ProfileImageURL));
            if (user.ProfileImageURL != null)
                ExistedUser.ProfileImageURL = user.ProfileImageURL;
            context.SaveChanges();
            return RedirectToAction("Timeline", "Home");
        }

        public ActionResult UserPosts(int? id)
        {
            ViewBag.uid = (int)Session["ID"];
            User user = context.Users.FirstOrDefault(u => u.Id == id);
            List<Post> posts = context.Posts.Include("Publisher").Include("Reacts").Include("Comments.User").Where(x => x.Publisher.Id == id).ToList();
            TimelineViewModel timeline = new TimelineViewModel();
            timeline.Posts = posts;
            timeline.loggedInUser = user;
            return View(timeline);
        }
    }
}