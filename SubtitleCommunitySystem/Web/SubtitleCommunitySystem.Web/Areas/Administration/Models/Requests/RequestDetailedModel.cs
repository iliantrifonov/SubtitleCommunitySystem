namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class RequestDetailedModel : IMapFrom<PromotionRequest>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public RequestType Type { get; set; }

        public RequestState RequestState { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PromotionRequest, RequestDetailedModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(u => u.User.UserName));
        }
    }
}