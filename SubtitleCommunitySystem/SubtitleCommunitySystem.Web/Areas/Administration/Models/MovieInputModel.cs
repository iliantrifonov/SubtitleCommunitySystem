namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class MovieInputModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Directory { get; set; }

        [Required]
        public string Name { get; set; }

        public string MainPosterUrl { get; set; }

        public string BannerUrl { get; set; }

        [Required]
        [Display(Name = "Released on:")]
        public DateTime ReleaseDate { get; set; }
    }
}