namespace SubtitleCommunitySystem.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class MovieDetailsViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string MainPosterUrl { get; set; }

        public string BannerUrl { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}