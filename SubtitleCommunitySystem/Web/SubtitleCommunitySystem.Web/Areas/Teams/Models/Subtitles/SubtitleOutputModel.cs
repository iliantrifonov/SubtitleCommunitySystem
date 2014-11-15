namespace SubtitleCommunitySystem.Web.Areas.Teams.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;
    using System.ComponentModel.DataAnnotations;

    public class SubtitleOutputModel : IMapFrom<Subtitle>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? TeamId { get; set; }

        [Display(Name = "Complete")]
        public bool IsFinished { get; set; }

        public virtual string Language { get; set; }

        public SubtitleState State { get; set; }

        [Display(Name = "Title")]
        public string MovieName { get; set; }

        [Display(Name = "Poster")]
        public string MovieMainPosterUrl { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Subtitle, SubtitleOutputModel>()
                .ForMember(m => m.Language, opt => opt.MapFrom(u => u.Language.Name))
                .ForMember(m => m.MovieName, opt => opt.MapFrom(u => u.Movie.Name))
                .ForMember(m => m.MovieMainPosterUrl, opt => opt.MapFrom(u => u.Movie.MainPosterUrl))
                .ForMember(m => m.TeamId, opt => opt.MapFrom(u => u.Team.Id));
        }
    }
}