namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class UserOutputModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}