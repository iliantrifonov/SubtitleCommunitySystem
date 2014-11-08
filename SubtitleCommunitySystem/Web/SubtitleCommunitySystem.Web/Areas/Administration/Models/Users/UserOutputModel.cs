namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;
    using System.ComponentModel.DataAnnotations;

    public class UserOutputModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}