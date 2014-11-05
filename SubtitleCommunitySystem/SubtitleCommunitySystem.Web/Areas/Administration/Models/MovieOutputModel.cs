using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    public class MovieOutputModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Name { get; set; }

        public string MainPosterUrl { get; set; }

        public string BannerUrl { get; set; }
    }
}