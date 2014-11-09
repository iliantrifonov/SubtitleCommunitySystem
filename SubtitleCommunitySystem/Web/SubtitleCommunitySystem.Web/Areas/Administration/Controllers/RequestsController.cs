namespace SubtitleCommunitySystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using SubtitleCommunitySystem.Web.Controllers;
using SubtitleCommunitySystem.Data;

    public class RequestsController : AdminController
    {
        public RequestsController(IApplicationData data) : base(data)
        {

        }

        // GET: Administration/Requests
        public ActionResult Index()
        {
            return View();
        }
    }
}