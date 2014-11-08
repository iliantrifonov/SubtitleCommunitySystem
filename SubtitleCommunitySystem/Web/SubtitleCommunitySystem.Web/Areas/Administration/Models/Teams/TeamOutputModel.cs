namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class TeamOutputModel : IMapFrom<Team>, IMapTo<Team>
    {
        public int Id { get; set; }

        public int Name { get; set; }
    }
}