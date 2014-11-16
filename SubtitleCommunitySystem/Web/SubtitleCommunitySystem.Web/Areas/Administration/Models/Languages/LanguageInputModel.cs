namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class LanguageInputModel : IMapFrom<Language>, IMapTo<Language>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}