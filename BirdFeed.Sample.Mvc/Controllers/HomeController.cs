using BirdFeed.Core;
using BirdFeed.Core.Request.Options;
using System.Web.Mvc;

namespace BirdFeed.Sample.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ITwitter twitter = BirdFeeder.Create();

            //twitter.Tweet("restlessmedia", "I love smell of demos in the morning. No-one ever said.");

            return View(twitter.Tweets("restlessmedia", 5));
        }
    }
}