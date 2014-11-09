namespace SubtitleCommunitySystem.Web.Areas.Private.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class RequestOutputModel : IMapFrom<PromotionRequest>
    {
        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public RequestType Type { get; set; }

        public RequestState RequestState { get; set; }
    }
}