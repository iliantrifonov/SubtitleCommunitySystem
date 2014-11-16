namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class TeamInputModel : IMapFrom<Team>, IMapTo<Team>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Language")]
        public string LanguageId { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Team, TeamInputModel>()
                .ForMember(m => m.LanguageId, opt => opt.MapFrom(u => u.Language.Id.ToString()));
        }
    }
}