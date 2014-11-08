namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;
    using System.ComponentModel.DataAnnotations;

    public class LanguageInputModel : IMapFrom<Language>, IMapTo<Language>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}