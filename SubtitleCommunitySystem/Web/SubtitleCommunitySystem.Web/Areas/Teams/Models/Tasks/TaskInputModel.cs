namespace SubtitleCommunitySystem.Web.Areas.Teams.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

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

        [HiddenInput(DisplayValue = false)]
        public int? FileId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<SubtitleTask, TaskInputModel>()
                .ForMember(t => t.UserName, opt => opt.MapFrom(z => z.User.UserName))
                .ForMember(t => t.FileId, opt => opt.MapFrom(z => z.FinishedPartFile.Id));
        }
    }
}