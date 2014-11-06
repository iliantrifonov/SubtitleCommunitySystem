namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class MovieOutputModel : IMapFrom<Movie>
    {
        public static Expression<Func<Movie, MovieOutputModel>> FromMovie
        {
            get
            {
                return m => new MovieOutputModel()
                {
                    BannerUrl = m.BannerUrl,
                    Description = m.Description,
                    Id = m.Id,
                    MainPosterUrl = m.MainPosterUrl,
                    Name = m.Name,
                    ReleaseDate = m.ReleaseDate
                };
            }
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string MainPosterUrl { get; set; }

        public string BannerUrl { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}