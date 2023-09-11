using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Twitche3.DataAccess;
using Twitche3.Models;
using Twitche3.Models.DataAccess;


namespace Twitche3.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private User currentUser;
        public ProfileController()
        {
            UserDAL udal = new UserDAL();
            currentUser = udal.GetUserByUsername(System.Web.HttpContext.Current.User.Identity.Name);
        }

        public ProfileController(string username)
        {
            UserDAL udal = new UserDAL();
            currentUser = udal.GetUserByUsername(username);
        }

        
        public ActionResult MyProfile()
        {
            return Index(currentUser.Id);
        }

        [Route("Profile/Index/{OwnerId}")]
        public ActionResult Index(string OwnerId)
        {
            UserDAL dal = new UserDAL();
            User user = dal.GetUser(OwnerId.ToUpper());
            var listFollowers = dal.GetFollowers(user.Id).ToArray();
            var listFollowing = dal.GetFollowing(user.Id).ToArray();

            object[] obj = { user.Bio, listFollowing.Length.ToString(), listFollowers.Length.ToString() };

            // GET FOLLOWING AND FOLLOWERS AND SEND IN ARRAY 

            ViewData["user"] = obj;

            ViewData["currentUser"] = user;

            TweetDAL ttdal = new TweetDAL();
            var list = ttdal.GetTweets();
            Tweet[] arr = list.ToArray();

            Tweet[] arr2 = arr.Where(s => s.OwnerId.Equals(user.Id.ToUpper())).ToArray();
            ViewData["tweets"] = arr2;

            return View("Index");
        }

            public ActionResult Edit()
        {
            //string userid = currentUser.Id;
            //UserDAL dal = new UserDAL();
            //var user = dal.GetUser(userid);

            ViewData["user"] = currentUser;

            return View();
        }

        [HttpPost]
        public ActionResult Update(User u)
        {
            UserDAL dal = new UserDAL();

            string userid = currentUser.Id;
            User current = dal.GetUser(userid);

            current.FirstName = u.FirstName != null ? u.FirstName : current.FirstName;
            current.LastName = u.LastName != null ? u.LastName : current.LastName;
            current.Email = u.Email != null ? u.Email : current.Email;
            current.Username = u.Username != null ? u.Username : current.Username;
            current.Password = u.Password != null ? u.Password : current.Password;
            current.Photo = u.Photo != null ? u.Photo : current.Photo;
            current.Bio = u.Bio != null ? u.Bio : current.Bio;

            dal.UpdateUser(current);

            ViewData["user"] = currentUser;
            return View("Edit");
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Files"), _FileName);
                    file.SaveAs(_path);
                    UserDAL dal = new UserDAL();
                    currentUser.Photo = _FileName;
                    dal.UpdateUser(currentUser);

                }
                ViewBag.Message = "File Uploaded Successfully!!";
            }
            catch(Exception e)
            {
                //Console.WriteLine(e.Message);
                ViewBag.Message = "File upload failed!!";
            }

            ViewData["user"] = currentUser;
            return View("Edit");
        }


    }
}