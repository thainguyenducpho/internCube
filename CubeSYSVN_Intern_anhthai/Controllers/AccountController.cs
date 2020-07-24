using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CubeSYSVN_Intern_anhthai.Models;
using System.Web.Security;
using WebMatrix.WebData;
using System.Data.Entity;

namespace CubeSYSVN_Intern_anhthai.Controllers
{
    public class AccountController : Controller
    {
        anhthaiEntities db = new anhthaiEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorise(SVR_USER user)
        {
            using (anhthaiEntities db = new anhthaiEntities())
            {
                var userDetail = db.SVR_USER.Where(x => x.USERNAME == user.USERNAME && x.PASSWORD == user.PASSWORD).FirstOrDefault();
                if (userDetail == null)
                {
                    user.LoginErrorMsg = "Invalid UserName or Password";
                    return View("Index", user);
                }
                else
                {
                    Session["userName"] = user.USERNAME;
                    return RedirectToAction("Index", "SVR_VIEW");
                }
            }

        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel obj)
        {
            string uid = Convert.ToString(Session["userName"]);
            SVR_USER u = db.SVR_USER.Find(uid);
            if (u.PASSWORD == obj.OldPassword)
            {
                if(obj.NewPassword == obj.ConfirmNewPassword)
                { 
                    u.PASSWORD = obj.NewPassword;
                    db.Entry(u).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Your Password is Update Successfully!";
                }
                else {
                    TempData["Message"] = "Your Password is Incorrect!";
                }
            }
            else
            {
                TempData["Message"] = "Invalid Current password!";

            }
            return RedirectToAction("Index", "SVR_VIEW", TempData["Message"]);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Account");
        }
    }
}