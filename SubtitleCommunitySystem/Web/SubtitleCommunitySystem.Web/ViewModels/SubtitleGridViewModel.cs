namespace SubtitleCommunitySystem.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class SubtitleGridViewModel : IMapFrom<Subtitle>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Language { get; set; }

        public string Team { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Subtitle, SubtitleGridViewModel>()
                .ForMember(m => m.Language, opt => opt.MapFrom(s => s.Language.Name))
                .ForMember(m => m.Team, opt => opt.MapFrom(s => s.Team.Name));
        }
    }
}