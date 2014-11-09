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
        private ApplicationUser currentUser;

        public BaseController(IApplicationData data)
        {
            this.Data = data;
        }

        protected IApplicationData Data { get; set; }

        protected ApplicationUser CurrentUser
        {
            get
            {
                if (this.currentUser != null)
                {
                    return this.currentUser;
                }

                if (this.User != null)
                {
                    var userId = User.Identity.GetUserId();

                    this.currentUser = this.Data.Users.Find(userId);

                    return this.currentUser;
                }

                return null;
            }
        }
    }
}