
namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Mappings;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class UserInputModel
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
                    .Select(t => new TeamRoleModel() { Id = t.Id, Name = t.Name }),
                };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<TeamRoleModel> TeamRoles { get; set; }
    }
}