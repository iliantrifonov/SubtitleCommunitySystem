namespace SubtitleCommunitySystem.Web.Areas.Teams.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;
    using AutoMapper;
    using System.Web.Mvc;

    public class TaskInputModel : IMapFrom<SubtitleTask>, IMapTo<SubtitleTask>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int PercentDone { get; set; }

        public DateTime DateCreated { get; set; }
        
        public DateTime DueDate { get; set; }

        public bool IsFinished { get; set; }

        public SubtitleTaskType Type { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? SubtitleId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<SubtitleTask, TaskInputModel>()
                .ForMember(t => t.UserName, opt => opt.MapFrom(z => z.User.UserName));
        }
    }
}