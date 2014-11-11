namespace SubtitleCommunitySystem.Web.Areas.Teams.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class SubtitleOutputModel : IMapFrom<Subtitle>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public bool IsFinished { get; set; }

        public virtual string Language { get; set; }

        public SubtitleState State { get; set; }

        public string MovieName { get; set; }

        public string MovieMainPosterUrl { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Subtitle, SubtitleOutputModel>()
                .ForMember(m => m.Language, opt => opt.MapFrom(u => u.Language.Name))
                .ForMember(m => m.MovieName, opt => opt.MapFrom(u => u.Movie.Name))
                .ForMember(m => m.MovieMainPosterUrl, opt => opt.MapFrom(u => u.Movie.MainPosterUrl))
                ;
        }
    }
}