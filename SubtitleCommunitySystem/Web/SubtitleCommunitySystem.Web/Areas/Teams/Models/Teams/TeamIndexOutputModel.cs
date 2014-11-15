namespace SubtitleCommunitySystem.Web.Areas.Teams.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class TeamIndexOutputModel : IMapFrom<Team>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Language { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Team, TeamIndexOutputModel>()
                .ForMember(m => m.Language, opt => opt.MapFrom(u => u.Language.Name));
        }
    }
}