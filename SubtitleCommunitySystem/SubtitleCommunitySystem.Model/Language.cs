namespace SubtitleCommunitySystem.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Language
    {
        public Language()
        {
            this.Teams = new HashSet<Team>();
            this.Subtitles = new HashSet<Subtitle>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Subtitle> Subtitles { get; set; }
    }
}
