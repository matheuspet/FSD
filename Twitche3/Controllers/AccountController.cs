using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Twitche3.DataAccess;
using Twitche3.Models;

namespace Twitche3.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {

            bool isValidUser = UserValidate.Login(model.Username, model.Password);

            if (isValidUser)
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();



        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {

            UserDAL dal = new UserDAL();
            if (dal.CreateUser(user))
            {
                return RedirectToAction("Login");
            }
            return View();

            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}