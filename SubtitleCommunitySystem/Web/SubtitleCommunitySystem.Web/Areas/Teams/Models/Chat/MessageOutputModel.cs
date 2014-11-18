namespace SubtitleCommunitySystem.Web.Areas.Teams.Models.Chat
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class MessageOutputModel : IMapFrom<Message>, IHaveCustomMappings
    {
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public string UserName { get; set; }

        public DateTime DateSent { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Message, MessageOutputModel>()
                .ForMember(c => c.UserName, opt => opt.MapFrom(z => z.User.UserName));
        }
    }
}