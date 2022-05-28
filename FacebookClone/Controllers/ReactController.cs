using FacebookClone.Data;
using FacebookClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacebookClone.Controllers
{
    public class ReactController : Controller
    {
        public AppDbContext context = new AppDbContext();
        public string addLike(int pid)
        {
            int Uid = (int)Session["ID"];
            var result = context.Reacts.FirstOrDefault(r => r.UserID == Uid && pid == r.PostId);
            if (result == null)
            {
                React react = new React();
                react.UserID = Uid;
                react.PostId = pid;
                react.Like = true;
                context.Reacts.Add(react);
                context.SaveChanges();
                return "1";
            }
            result.Like = true;
            context.SaveChanges();
            return "1";
        }

        public string removeLike(int pid)
        {
            int Uid = (int)Session["ID"];
            var result = context.Reacts.Remove(context.Reacts.FirstOrDefault(r => r.UserID == Uid && pid == r.PostId));
            context.SaveChanges();
            return "1";
        }

        public string addDislike(int pid)
        {
            int Uid = (int)Session["ID"];
            var result = context.Reacts.FirstOrDefault(r => r.UserID == Uid && pid == r.PostId);
            if (result == null)
            {
                React react = new React();
                react.UserID = Uid;
                react.PostId = pid;
                react.Like = false;
                context.Reacts.Add(react);
                context.SaveChanges();
                return "1";
            }
            result.Like = false;
            context.SaveChanges();
            return "1";
        }

        public string removeDislike(int pid)
        {
            int Uid = (int)Session["ID"];
            var result = context.Reacts.Remove(context.Reacts.FirstOrDefault(r => r.UserID == Uid && pid == r.PostId));
            context.SaveChanges();
            return "1";
        }
    }
}