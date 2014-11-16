namespace SubtitleCommunitySystem.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class SubtitleCacheSearchViewModel : IMapFrom<Subtitle>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Language { get; set; }

        public string MovieName { get; set; }

        public int MovieId { get; set; }

        public DateTime ReleaseDate { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Subtitle, SubtitleCacheSearchViewModel>()
                .ForMember(m => m.Language, opt => opt.MapFrom(z => z.Language.Name))
                .ForMember(m => m.MovieName, opt => opt.MapFrom(z => z.Movie.Name))
                .ForMember(m => m.MovieId, opt => opt.MapFrom(z => z.Movie.Id))
                .ForMember(m => m.ReleaseDate, opt => opt.MapFrom(z => z.Movie.ReleaseDate));
        }
    }
}