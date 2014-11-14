namespace SubtitleCommunitySystem.Web.Services
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Web.Caching;
    using System.Web;

    using AutoMapper.QueryableExtensions;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Areas.Teams.Models;

    public class CacheService : ICacheService
    {
        private const string UserCacheDropDownFormat = "users:{0}:{1}";

        public CacheService(IApplicationData data)
        {
            this.Data = data;

        }

        protected IApplicationData Data { get; set; }

        public IEnumerable GetDropDownForUsers(string roleName, int? teamId)
        {
            var cacheKey = string.Format(UserCacheDropDownFormat, roleName, teamId);

            var users = HttpContext.Current.Cache.Get(cacheKey);
            if (users == null)
            {
                users = CacheUsersDropDown(roleName, teamId);
                HttpContext.Current.Cache.Add(cacheKey, users, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            return users as IEnumerable;
        }

        private IEnumerable CacheUsersDropDown(string roleName, int? teamId)
        {
            return this.Data.Users.All()
                .Where(u => u.Teams.Any(t => t.Id == teamId))
                .Where(u => u.TeamRoles.Any(r => r.Name == roleName))
                .Project().To<UserDropDownModel>()
                .ToList();
        }

    }
}