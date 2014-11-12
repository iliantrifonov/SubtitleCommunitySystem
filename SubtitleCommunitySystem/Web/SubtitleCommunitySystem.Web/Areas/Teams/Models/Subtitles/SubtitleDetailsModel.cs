namespace SubtitleCommunitySystem.Web.Areas.Teams.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class SubtitleDetailsModel : IMapFrom<Subtitle>, IMapTo<Subtitle>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string MovieMainPosterUrl { get; set; }

        public string MovieName { get; set; }

        public string MovieDescription { get; set; }

        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public bool IsFinished { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Subtitle, SubtitleDetailsModel>()
                .ForMember(m => m.MovieName, opt => opt.MapFrom(u => u.Movie.Name))
                .ForMember(m => m.MovieDescription, opt => opt.MapFrom(u => u.Movie.Description))
                .ForMember(m => m.MovieMainPosterUrl, opt => opt.MapFrom(u => u.Movie.MainPosterUrl));
       
        }
    }
}