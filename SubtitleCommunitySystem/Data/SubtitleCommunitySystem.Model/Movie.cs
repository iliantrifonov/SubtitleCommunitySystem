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

        [MaxLength(3000)]
        public string Description { get; set; }

        [MaxLength(500)]
        public string Directory { get; set; }

        public int Id { get; set; }

        public virtual ICollection<Subtitle> Subtitles { get; set; }

        [MaxLength(500)]
        public string BannerUrl { get; set; }

        [MaxLength(500)]
        public string MainPosterUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual DbFile InitialSource { get; set; }
    }
}
