namespace SubtitleCommunitySystem.Web.Areas.Teams.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class TeamDetailedOutputModel : IMapFrom<Team>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Language { get; set; }

        public int ChatChannelId { get; set; }

        public IEnumerable<string> Translators { get; set; }

        public IEnumerable<string> ImageManagers { get; set; }

        public IEnumerable<string> Syncs { get; set; }

        public IEnumerable<string> Revisioners { get; set; }

        public IEnumerable<string> TeamLeaders { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Team, TeamDetailedOutputModel>()
                .ForMember(m => m.Language, opt => opt.MapFrom(u => u.Language.Name))
                .ForMember(m => m.Translators, opt => opt.MapFrom(u => u.Members.Where(usr => usr.TeamRoles.Any(r => r.Name == Infrastructure.Constants.RoleConstants.Translator)).Select(a => a.UserName)))
                .ForMember(m => m.ImageManagers, opt => opt.MapFrom(u => u.Members.Where(usr => usr.TeamRoles.Any(r => r.Name == Infrastructure.Constants.RoleConstants.ImageManager)).Select(a => a.UserName)))
                .ForMember(m => m.Syncs, opt => opt.MapFrom(u => u.Members.Where(usr => usr.TeamRoles.Any(r => r.Name == Infrastructure.Constants.RoleConstants.Sync)).Select(a => a.UserName)))
                .ForMember(m => m.Revisioners, opt => opt.MapFrom(u => u.Members.Where(usr => usr.TeamRoles.Any(r => r.Name == Infrastructure.Constants.RoleConstants.Revisioner)).Select(a => a.UserName)))
                .ForMember(m => m.TeamLeaders, opt => opt.MapFrom(u => u.Members.Where(usr => usr.TeamRoles.Any(r => r.Name == Infrastructure.Constants.RoleConstants.TeamLeader)).Select(a => a.UserName)))
                .ForMember(m => m.ChatChannelId, opt => opt.MapFrom(u => u.ChatChannel.Id));
        }
    }
}