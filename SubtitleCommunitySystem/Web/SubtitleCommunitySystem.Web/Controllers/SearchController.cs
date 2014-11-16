namespace SubtitleCommunitySystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Web.Services;

    public class SearchController : BaseController
    {
        private ICacheService cacheService;

        public SearchController(IApplicationData data, ICacheService cache)
            : base(data)
        {
            this.cacheService = cache;
        }

        public ActionResult Index(string q)
        {
            this.ViewBag.SearchQuery = q;
            return this.View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = (30 * 60), VaryByParam = "searchString")]
        public ActionResult GetResults(string searchString)
        {
            var searchResults = this.cacheService.GetTop200SearchResults(searchString);

            return this.PartialView("_SearchResultsPartial", searchResults);
        }
    }
}