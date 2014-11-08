namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;

    public class UserInputModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
        public static Expression<Func<ApplicationUser, UserInputModel>> ToModel
        {
            get
            {
                return u => new UserInputModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    TeamRoles = u.TeamRoles.AsQueryable()
                    .Select(t => new TeamRoleModel()
                    {
                        Id = t.Id,
                        Name = t.Name
                    }),
                    Teams = u.Teams.AsQueryable().Select(TeamViewModel.FromTeam)
                };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<TeamViewModel> Teams { get; set; }

        public IEnumerable<TeamRoleModel> TeamRoles { get; set; }
    }
}