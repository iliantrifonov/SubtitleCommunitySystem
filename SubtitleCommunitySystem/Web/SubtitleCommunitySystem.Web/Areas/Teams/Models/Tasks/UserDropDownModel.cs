namespace SubtitleCommunitySystem.Web.Areas.Teams.Models
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class UserDropDownModel : IMapFrom<ApplicationUser>
    {
        [HiddenInput(DisplayValue = false)]

        public string Id { get; set; }

        public string UserName { get; set; }
    }
}