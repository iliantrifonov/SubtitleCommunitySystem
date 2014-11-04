namespace SubtitleCommunitySystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Team
    {
        public Team()
        {
            this.Members = new HashSet<ApplicationUser>();
            this.Subtitles = new HashSet<Subtitle>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Language Language { get; set; }

        public virtual ICollection<ApplicationUser> Members { get; set; }

        public virtual ICollection<Subtitle> Subtitles { get; set; }
    }
}
