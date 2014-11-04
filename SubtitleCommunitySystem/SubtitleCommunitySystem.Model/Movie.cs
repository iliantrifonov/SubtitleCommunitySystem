namespace SubtitleCommunitySystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Movie
    {
        public Movie()
        {
            this.Subtitles = new HashSet<Subtitle>();
        }

        public int Id { get; set; }

        public virtual ICollection<Subtitle> Subtitles { get; set; }

        public virtual DbFile BannerFile { get; set; }

        public virtual DbFile MainPoster { get; set; }
    }
}
