namespace SubtitleCommunitySystem.Web.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using SubtitleCommunitySystem.Data;

    public class AuthFilter : IAuthorizationFilter
    {
        private readonly string[] roles;
        private IApplicationData data;

        public AuthFilter(IApplicationData data, string[] roles)
        {
            this.data = data;
            this.roles = roles;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var userId = filterContext.HttpContext.User.Identity.GetUserId();

            bool containsRole = this.data.Users.All()
                                   .Where(u => u.Id == userId)
                                   .Any(usr => usr.TeamRoles.Any(r => this.roles.Contains(r.Name)));

            if (!containsRole)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
        }
    }
}