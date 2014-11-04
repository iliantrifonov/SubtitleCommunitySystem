namespace SubtitleCommunitySystem.Model
{
    using System.Collections.Generic;

    public class Subtitle
    {
        public Subtitle()
        {
            this.Tasks = new HashSet<SubtitleTask>();
        }

        public int Id { get; set; }

        public bool IsFinished { get; set; }

        public virtual Language Language { get; set; }

        public virtual Team Team { get; set; }

        public SubtitleState State { get; set; }

        public virtual DbFile InitialSource { get; set; }

        public virtual DbFile PartialFile { get; set; }

        public virtual DbFile FinalFile { get; set; }

        public virtual ICollection<SubtitleTask> Tasks { get; set; }
    }
}
