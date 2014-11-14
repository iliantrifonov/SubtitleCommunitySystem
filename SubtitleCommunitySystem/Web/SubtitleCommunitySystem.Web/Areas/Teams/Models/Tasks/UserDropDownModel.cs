namespace SubtitleCommunitySystem.Web.Areas.Teams.Models
{
    using System;
    using System.Linq;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class UserDropDownModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        //public void CreateMappings(AutoMapper.IConfiguration configuration)
        //{
        //    configuration.CreateMap<ApplicationUser, UserDropDownModel>()
        //        .ForMember(m => m.UserId, opt => opt.MapFrom(z => z.Id));
        //}
    }
}