namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class TeamRoleModel : IMapFrom<TeamRole>, IMapTo<TeamRole>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}