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
    public class FeedController : Controller
    {

        [Authorize]
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
            Console.WriteLine("LOOOOOOOOOOOOOGUEEEEEEEEEE- cheguei no post");

            // AQUI CHEGA O TWEET SÓ COM A DESCRIPTION, PRECISA POPULAR O RESTO COM AS INFORMAÇÕES DO USUARIO
            tweet.Owner = "Matheus Mattos";
            tweet.OwnerId = "6049102F-062D-46B7-8986-F3FA941CA9E6";

            TweetDAL dal = new TweetDAL();
            dal.CreateTweet(tweet);

            return RedirectToAction("Index", "Feed");
        }


        [Route("Feed/Like/{id}")]
        public ActionResult Like(string id)
        {
            Console.WriteLine("LOOOOOOOOOOOOOGUEEEEEEEEEE- cheguei no LIKEEE");
            Tweet tweet = new Tweet();
            tweet.Id = id;
            Console.WriteLine("Eu sou o Tweet: " + tweet);
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