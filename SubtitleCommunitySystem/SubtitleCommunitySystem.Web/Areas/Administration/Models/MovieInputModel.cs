namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    using SubtitleCommunitySystem.Model;
    using System.ComponentModel.DataAnnotations;

    public class MovieInputModel
    {
        public static Movie ToMovie(MovieInputModel m)
        {
            return new Movie()
                   {
                       BannerUrl = m.BannerUrl,
                       Description = m.Description,
                       Directory = m.Directory,
                       MainPosterUrl = m.MainPosterUrl,
                       Name = m.Name,
                       ReleaseDate = m.ReleaseDate
                   };
        }

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