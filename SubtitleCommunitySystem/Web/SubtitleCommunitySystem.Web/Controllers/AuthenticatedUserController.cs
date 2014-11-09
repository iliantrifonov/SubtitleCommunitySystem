namespace SubtitleCommunitySystem.Web.Controllers
{
    using SubtitleCommunitySystem.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    public class AuthenticatedUserController : BaseController
    {
        public AuthenticatedUserController(IApplicationData data)
            : base(data)
        {
            //TODO: MyRequests controller
        }

        // GET: AuthenticatedUser
        public ActionResult Index()
        {
            return View();
        }
    }
}