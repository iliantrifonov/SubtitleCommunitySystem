namespace SubtitleCommunitySystem.Model
{
    using System.Collections.Generic;

    public class Language
    {
        public Language()
        {
            this.Teams = new HashSet<Team>();
            this.Subtitles = new HashSet<Subtitle>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Subtitle> Subtitles { get; set; }
    }
}
