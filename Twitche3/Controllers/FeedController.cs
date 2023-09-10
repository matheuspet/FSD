using System;
using System.Collections.Generic;
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
    public class FeedController : Controller
    {

        


        private User currentUser;

        public FeedController()
        {
            UserDAL udal = new UserDAL();
            currentUser = udal.GetUserByUsername(System.Web.HttpContext.Current.User.Identity.Name);
        }


        public ActionResult Index()
        {

            TweetDAL dal = new TweetDAL();
            var list = dal.GetTweets();


            Tweet[] arr = list.ToArray();


            ViewData["tweets"] = arr;

            Tweet[] arr2 = arr.Where(s => s.OwnerId.Equals("1")).ToArray();
            ViewData["idfilter"] = arr2;

            Tweet[] arr3 = arr.Where(s => s.OwnerId.Contains("Tweet")).ToArray();

            ViewData["emailfilter"] = arr3;

            return View();
        }

        public ActionResult Post(Tweet tweet)
        {

            // AQUI CHEGA O TWEET SÓ COM A DESCRIPTION, PRECISA POPULAR O RESTO COM AS INFORMAÇÕES DO USUARIO
            tweet.Owner = currentUser.Name;
            tweet.OwnerId = currentUser.Id.ToUpper();

            TweetDAL dal = new TweetDAL();
            dal.CreateTweet(tweet);

            return RedirectToAction("Index", "Feed");
        }


        [Route("Feed/Like/{id}")]
        public ActionResult Like(string id)
        {
            Tweet tweet = new Tweet();
            tweet.Id = id;
            TweetDAL dal = new TweetDAL();
            dal.LikeTweet(tweet.Id);

            return RedirectToAction("Index", "Feed");
        }

        [Route("Feed/Retweet/{id}")]
        public ActionResult Retweet(string id)
        {

            Tweet reTweet = new Tweet();
            TweetDAL dal = new TweetDAL();
            Tweet originalTweet = dal.GetTweet(id);
            reTweet.Description = "'" + originalTweet.Description + "' by " + originalTweet.Owner;
            Post(reTweet);
            dal.ReTweet(id);

            return RedirectToAction("Index", "Feed");
        }

    }
}