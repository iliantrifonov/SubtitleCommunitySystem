namespace SubtitleCommunitySystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Movie
    {
        public Movie()
        {
            this.Subtitles = new HashSet<Subtitle>();
        }

        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Directory { get; set; }

        public int Id { get; set; }

        public virtual ICollection<Subtitle> Subtitles { get; set; }

        public string BannerUrl { get; set; }

        public string MainPosterUrl { get; set; }
    }
}
