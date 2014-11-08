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

            if (this.User != null)
            {
                var userId = User.Identity.GetUserId();

                this.CurrentUser = data.Users.Find(userId);
            }
        }

        protected IApplicationData Data { get; set; }

        protected ApplicationUser CurrentUser { get; set; }
    }
}