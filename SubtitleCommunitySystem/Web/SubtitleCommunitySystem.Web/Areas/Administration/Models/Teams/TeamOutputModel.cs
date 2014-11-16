namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class TeamOutputModel : IMapFrom<Team>, IMapTo<Team>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Language { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Team, TeamOutputModel>()
                .ForMember(m => m.Language, opt => opt.MapFrom(u => u.Language.Name));
        }
    }
}