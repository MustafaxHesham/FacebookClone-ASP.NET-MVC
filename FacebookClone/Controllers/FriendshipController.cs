using FacebookClone.Data;
using FacebookClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacebookClone.Controllers
{
    public class FriendshipController : Controller
    {
        public AppDbContext context = new AppDbContext();
        public ActionResult FriendRequests()
        {
            int uid = (int)Session["ID"];
            return View("Notifications", context.Friendships.Include("User1").Include("User2").Where(fs => fs.User2ID == uid && fs.IsFriend == null));
        }

        //Ajax
        public string SendFriendRequest(int requestedFriend)
        {
            Friendship friendship = new Friendship();
            friendship.User1ID = (int)Session["ID"];
            friendship.User2ID = requestedFriend;
            friendship.IsFriend = null;
            var result = context.Friendships.Add(friendship);
            context.SaveChanges();
            return (result != null) ? "done" : "error";
        }

        //Ajax
        public string AcceptFriendRequest(int SenderReqId)
        {
            int CurrentLoggedUserId = (int)Session["ID"];
            var friendship = context.Friendships.FirstOrDefault(fs => fs.User1ID == SenderReqId && fs.User2ID == CurrentLoggedUserId);
            friendship.IsFriend = true;
            context.SaveChanges();
            return (friendship != null) ? "done" : "error";
        }

        //Ajax
        public string RefuseFriendRequest(int SenderReqId)
        {
            int CurrentLoggedUserId = (int)Session["ID"];
            var friendship = context.Friendships.FirstOrDefault(fs => fs.User1ID == SenderReqId && fs.User2ID == CurrentLoggedUserId);
            context.Friendships.Remove(friendship);
            context.SaveChanges();
            return (friendship != null) ? "done" : "error";
        }
        public bool IsFriend(int id)
        {
            int Uid = (int)Session["ID"];
            return Uid == id;
        }
    }
}