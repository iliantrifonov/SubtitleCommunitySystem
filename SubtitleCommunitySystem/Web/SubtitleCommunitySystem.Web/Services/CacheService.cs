namespace SubtitleCommunitySystem.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;
    using System.Web.Caching;
    using System.Web;

    using AutoMapper.QueryableExtensions;
    using Gma.DataStructures.StringSearch;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Areas.Teams.Models;
    using SubtitleCommunitySystem.Web.ViewModels;

    public class CacheService : ICacheService
    {
        private const string SubtitleSearchCacheKey = "subtitleSearch";
        private const string UserCacheDropDownFormat = "users:{0}:{1}";

        public CacheService(IApplicationData data)
        {
            this.Data = data;
            this.CacheSubtitlesSearch();
        }

        protected ITrie<SubtitleCacheSearchViewModel> Trie { get; set; }

        protected IApplicationData Data { get; set; }

        public IEnumerable GetDropDownForUsers(string roleName, int? teamId)
        {
            var cacheKey = string.Format(UserCacheDropDownFormat, roleName, teamId);

            var users = HttpContext.Current.Cache.Get(cacheKey);
            if (users == null)
            {
                users = this.GetUsersDropDownForCaching(roleName, teamId);
                HttpContext.Current.Cache.Add(cacheKey, users, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            return users as IEnumerable;
        }
        
        public IEnumerable GetTop200SearchResults(string searchString)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var patternToLowerCaseInvariant = searchString.ToLowerInvariant();
                var trie = HttpContext.Current.Cache[SubtitleSearchCacheKey] as ITrie<SubtitleCacheSearchViewModel>;
                var foundResult = trie.Retrieve(patternToLowerCaseInvariant).Take(200).ToList();

                return foundResult;
            }

            return new List<SubtitleCacheSearchViewModel>();
        }

        protected virtual ITrie<SubtitleCacheSearchViewModel> CreateTrie()
        {
            return new PatriciaSuffixTrie<SubtitleCacheSearchViewModel>(1);
        }

        private IEnumerable GetUsersDropDownForCaching(string roleName, int? teamId)
        {
            return this.Data.Users.All()
                .Where(u => u.Teams.Any(t => t.Id == teamId))
                .Where(u => u.TeamRoles.Any(r => r.Name == roleName))
                .Project().To<UserDropDownModel>()
                .ToList();
        }

        private void PopulateTrieWithSubtitleSearch(IList<SubtitleCacheSearchViewModel> subtitleModels)
        {
            this.Trie = this.CreateTrie();
            for (int i = 0; i < subtitleModels.Count; i++)
            {
                var currentModel = subtitleModels[i];
                
                var currentLanguageToLowerInvariant = currentModel.Language.ToLowerInvariant();
                this.Trie.Add(currentLanguageToLowerInvariant, currentModel);

                var currentMovieNameToLowerInvariant = currentModel.MovieName.ToLowerInvariant();
                this.Trie.Add(currentMovieNameToLowerInvariant, currentModel);

                var currentSubNameToLowerInvariant = currentModel.Name.ToLowerInvariant();
                this.Trie.Add(currentSubNameToLowerInvariant, currentModel);
            }
        }

        private void OnRemoveCallback(string key, object value, CacheItemRemovedReason reason)
        {
            this.CacheSubtitlesSearch();
        }

        private void CacheSubtitlesSearch()
        {
            if (HttpContext.Current.Cache[SubtitleSearchCacheKey] == null)
            {
                var subtitleModels = this.Data.Subtitles.All()
                    .Where(c => c.IsFinished == true)
                    .Project().To<SubtitleCacheSearchViewModel>()
                    .ToList();

                this.PopulateTrieWithSubtitleSearch(subtitleModels);
                HttpContext.Current.Cache.Insert(SubtitleSearchCacheKey, this.Trie, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration, CacheItemPriority.Default, this.OnRemoveCallback);
            }
        }
    }
}