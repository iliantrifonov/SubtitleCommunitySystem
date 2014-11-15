namespace SubtitleCommunitySystem.Web.Areas.Private.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web;

    using AutoMapper;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class TaskOutputModel : IMapFrom<SubtitleTask>, IHaveCustomMappings
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
        public int? FileId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<SubtitleTask, TaskOutputModel>()
                .ForMember(t => t.FileId, opt => opt.MapFrom(z => z.FinishedPartFile.Id));
        }
    }
}