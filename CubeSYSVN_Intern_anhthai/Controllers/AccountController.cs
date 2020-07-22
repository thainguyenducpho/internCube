using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CubeSYSVN_Intern_anhthai.Models;
using System.Web.Security;
using WebMatrix.WebData;

namespace CubeSYSVN_Intern_anhthai.Controllers
{
    public class AccountController : Controller
    {
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

        [HttpGet, Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                bool isPasswordChanged = WebSecurity.ChangePassword(WebSecurity.CurrentUserName, changePasswordModel.OldPassword, changePasswordModel.NewPassword);

                if (isPasswordChanged)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Old Password is not correct.");
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Account");
        }
    }
}