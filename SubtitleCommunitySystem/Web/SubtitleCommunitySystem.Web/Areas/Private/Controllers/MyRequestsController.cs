namespace SubtitleCommunitySystem.Web.Areas.Private.Controllers
{
    using SubtitleCommunitySystem.Web.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class MyRequestsController : AuthenticatedUserController
    {
        // GET: Private/MyRequests
        public ActionResult Index()
        {
            return View();
        }
    }
}