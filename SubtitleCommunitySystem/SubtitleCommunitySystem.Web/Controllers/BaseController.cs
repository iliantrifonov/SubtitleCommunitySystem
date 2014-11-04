namespace SubtitleCommunitySystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;

    public class BaseController : Controller
    {
        public BaseController(IApplicationData data)
        {
            this.Data = data;
            var userId = User.Identity.GetUserId();
            if (userId != null)
            {
                this.CurrentUser = data.Users.Find(userId);
            }
        }

        protected IApplicationData Data { get; set; }

        protected ApplicationUser CurrentUser { get; set; }
    }
}