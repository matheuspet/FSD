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

        /*
        public ActionResult Index()
        {
            UserDAL dal = new UserDAL();

            var listFollowers = dal.GetFollowers(currentUser.Id).ToArray();
            var listFollowing = dal.GetFollowing(currentUser.Id).ToArray();

            object[] obj = { currentUser.Bio, listFollowing.Length.ToString(), listFollowers.Length.ToString() };

            // GET FOLLOWING AND FOLLOWERS AND SEND IN ARRAY 

            ViewData["user"] = obj;

            TweetDAL ttdal = new TweetDAL();
            var list = ttdal.GetTweets();
            Tweet[] arr = list.ToArray();

            Tweet[] arr2 = arr.Where(s => s.OwnerId.Equals(currentUser.Id.ToUpper())).ToArray();
            ViewData["tweets"] = arr2;

            return View();
        }*/

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

            TweetDAL ttdal = new TweetDAL();
            var list = ttdal.GetTweets();
            Tweet[] arr = list.ToArray();

            Tweet[] arr2 = arr.Where(s => s.OwnerId.Equals(user.Id.ToUpper())).ToArray();
            ViewData["tweets"] = arr2;

            return View();
        }

            public ActionResult Edit()
        {
            string userid = currentUser.Id;
            UserDAL dal = new UserDAL();
            var user = dal.GetUser(userid);

            ViewData["user"] = user;

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

            return RedirectToAction("Index");
        }

        /*
        public void SavePhoto()
        {
            //save photo
            if (u.Photo != null)
            {
                string strFileName;
                string strFilePath;
                string strFolder;
                strFolder = Server.MapPath("./");
                // Retrieve the name of the file that is posted.
                strFileName = u.Photo;
                strFileName = Path.GetFileName(strFileName);
                if (oFile.Value != "")
                {
                    // Create the folder if it does not exist.
                    if (!Directory.Exists(strFolder))
                    {
                        Directory.CreateDirectory(strFolder);
                    }
                    // Save the uploaded file to the server.
                    strFilePath = strFolder + strFileName;
                    if (File.Exists(strFilePath))
                    {
                        lblUploadResult.Text = strFileName + " already exists on the server!";
                    }
                    else
                    {
                        oFile.PostedFile.SaveAs(strFilePath);
                        lblUploadResult.Text = strFileName + " has been successfully uploaded.";
                    }
                }
                else
                {
                    lblUploadResult.Text = "Click 'Browse' to select the file to upload.";
                }
            }
        }*/


    }
}