namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    using SubtitleCommunitySystem.Model;

    public class MovieInputModel
    {
        public static Expression<Func<MovieInputModel, Movie>> FromMovie
        {
            get
            {
                return m => new Movie()
                {
                    BannerUrl = m.BannerUrl,
                    Description = m.Description,
                    Directory = m.Directory,
                    MainPosterUrl = m.MainPosterUrl,
                    Name = m.Name,
                };
            }
        }

        public string Description { get; set; }

        public string Directory { get; set; }

        public string Name { get; set; }

        public string MainPosterUrl { get; set; }

        public string BannerUrl { get; set; }
    }
}