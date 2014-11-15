namespace SubtitleCommunitySystem.Web.Areas.Teams.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class SubtitleDetailsModel : IMapFrom<Subtitle>, IMapTo<Subtitle>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string MovieMainPosterUrl { get; set; }

        public string MovieName { get; set; }

        public string MovieDescription { get; set; }

        [Required]
        [MaxLength(70)]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }

        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public bool IsFinished { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? DescriptionFileId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? SubtitleFileId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Subtitle, SubtitleDetailsModel>()
                .ForMember(m => m.MovieName, opt => opt.MapFrom(u => u.Movie.Name))
                .ForMember(m => m.DescriptionFileId, opt => opt.MapFrom(u => u.PartialFile.Id))
                .ForMember(m => m.SubtitleFileId, opt => opt.MapFrom(u => u.FinalFile.Id))
                .ForMember(m => m.MovieDescription, opt => opt.MapFrom(u => u.Movie.Description))
                .ForMember(m => m.MovieMainPosterUrl, opt => opt.MapFrom(u => u.Movie.MainPosterUrl));
       
        }
    }
}