namespace SubtitleCommunitySystem.Web.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class AuthAttribute : FilterAttribute
    {
        public string[] Roles { get; set; }

        public AuthAttribute(params string[] roles)
        {
            this.Roles = roles;
        }
    }
}