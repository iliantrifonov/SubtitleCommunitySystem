namespace SubtitleCommunitySystem.Web.Areas.Teams.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class TaskInputModel : IMapFrom<SubtitleTask>, IMapTo<SubtitleTask>
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int PercentDone { get; set; }

        public DateTime DateCreated { get; set; }
        
        public DateTime DueDate { get; set; }

        public bool IsFinished { get; set; }

        public SubtitleTaskType Type { get; set; }

        public int? SubtitleId { get; set; }
    }
}