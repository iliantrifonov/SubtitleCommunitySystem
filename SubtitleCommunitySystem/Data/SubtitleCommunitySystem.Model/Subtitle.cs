namespace SubtitleCommunitySystem.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Subtitle
    {
        public Subtitle()
        {
            this.Tasks = new HashSet<SubtitleTask>();
        }

        [Required]
        public virtual Movie Movie { get; set; }

        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        public int Id { get; set; }

        public bool IsFinished { get; set; }

        public virtual Language Language { get; set; }

        public virtual Team Team { get; set; }

        public SubtitleState State { get; set; }

        public virtual DbFile PartialFile { get; set; }

        public virtual DbFile FinalFile { get; set; }

        public virtual ICollection<SubtitleTask> Tasks { get; set; }
    }
}
