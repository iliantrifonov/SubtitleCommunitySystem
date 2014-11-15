namespace SubtitleCommunitySystem.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    
    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class SubtitleDetailViewModel : IMapFrom<Subtitle>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string MainPosterUrl { get; set; }

        public string BannerUrl { get; set; }

        public string Language { get; set; }

        public string TeamName { get; set; }

        public int? FileId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Subtitle, SubtitleDetailViewModel>()
                .ForMember(m => m.MainPosterUrl, opt => opt.MapFrom(c => c.Movie.MainPosterUrl))
                .ForMember(m => m.BannerUrl, opt => opt.MapFrom(c => c.Movie.BannerUrl))
                .ForMember(m => m.Language, opt => opt.MapFrom(c => c.Language.Name))
                .ForMember(m => m.TeamName, opt => opt.MapFrom(c => c.Team.Name))
                .ForMember(m => m.FileId, opt => opt.MapFrom(c => c.FinalFile.Id));
        }
    }
}