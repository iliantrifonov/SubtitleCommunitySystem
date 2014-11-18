namespace SubtitleCommunitySystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}