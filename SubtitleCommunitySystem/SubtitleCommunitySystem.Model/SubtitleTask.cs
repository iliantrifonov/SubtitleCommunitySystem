namespace SubtitleCommunitySystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class SubtitleTask
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int PercentDone { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsFinished { get; set; }

        public virtual DbFile FinishedPartFile { get; set; }

        public SubtitleTaskType Type { get; set; }
    }
}
