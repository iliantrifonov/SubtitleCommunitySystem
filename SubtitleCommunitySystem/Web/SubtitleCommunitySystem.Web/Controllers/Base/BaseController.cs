namespace SubtitleCommunitySystem.Web.Controllers.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;
    using System.Web.Routing;

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

        //protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        //{
        //    return base.BeginExecute(requestContext, callback, state);
        //}

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            // Work with data before BeginExecute to prevent "NotSupportedException: A second operation started on this context before a previous asynchronous operation completed."
            var userId = requestContext.HttpContext.User.Identity.GetUserId();

            IEnumerable<string> userRoles = this.Data.Users.All()
                .Where(u => u.Id == userId)
                .Select(usr => usr.TeamRoles.Select(r => r.Name))
                .FirstOrDefault();
            
            this.ViewBag.UserRoles = userRoles;

            // Calling BeginExecute before PrepareSystemMessages for the TempData to has values
            var result = base.BeginExecute(requestContext, callback, state);
            return result;
        }
    }
}