namespace SubtitleCommunitySystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.ViewModels;
    using SubtitleCommunitySystem.Web.Filters;

    public class HomeController : BaseController
    {
        public HomeController(IApplicationData data) : base(data)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [OutputCache(Duration = (10 * 60), VaryByParam = "page")]
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = this.Data.Movies.All()
                .Project().To<MovieListViewModel>()
                .ToDataSourceResult(request);
            return Json(data);
        }

        public ActionResult MovieDetails(int? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Movie Id not valid");
            }

            var movie = this.Data.Movies.All()
                .Where(m => m.Id == id)
                .Project().To<MovieDetailsViewModel>()
                .FirstOrDefault();

            if (movie == null)
            {
                throw new HttpException(404, "Movie not found!");
            }

            return View(movie);
        }

        [ChildActionOnly]
        [ParameterName(ViewParameterName = "grid-page", ActionParameterName = "page", Order = 1)]
        [OutputCache(VaryByParam = "movieId;page", Duration = (1 * 60 * 60), Order = 2)]
        public ActionResult SubtitleList(int? movieId, string page)
        {
            var subtitles = this.Data.Subtitles.All()
                .Where(s => s.Movie.Id == movieId)
                .Where(s => s.IsFinished)
                .Project().To<SubtitleGridViewModel>()
                .ToList();

            return this.PartialView("_SubtitlesGridViewPartial", subtitles);
        }

        public ActionResult SubtitleDetails(int? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Incorrect route parameters, id cannot be null");
            }

            int? fileId = this.Data.Subtitles.All()
                .Where(c => c.Id == id)
                .Select(c => c.FinalFile.Id).FirstOrDefault();

            if (fileId == null)
            {
                throw new HttpException(404, "Subtitle does not exist, or does not have a subtitle file.");                
            }

            ViewBag.SubtitleId = id;
            ViewBag.FileId = fileId;

            return View();
        }

        [HttpGet]
        [OutputCache(Duration = (1 * 60 * 60), VaryByParam = "subtitleId")]
        public ActionResult GetDetailsPartial(int? subtitleId)
        {
            
            if (subtitleId == null)
            {
                throw new HttpException(400, "Incorrect route parameters, subtitleId cannot be null");
            }

            var subtitle = this.Data.Subtitles.All()
                .Where(s => s.Id == subtitleId)
                .Project().To<SubtitleDetailViewModel>()
                .FirstOrDefault();

            if (subtitle == null)
            {
                throw new HttpException(404, "Subtitle does not exist!");
            }

            return this.PartialView("_SubtitleDetailsPartial", subtitle);
        }
    }
}