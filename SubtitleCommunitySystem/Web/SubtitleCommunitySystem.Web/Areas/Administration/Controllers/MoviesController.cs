namespace SubtitleCommunitySystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Areas.Administration.Models;
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Web.Helpers;

    public class MoviesController : AdminController
    {
        public MoviesController(IApplicationData data) : base(data)
        {
        }

        public ActionResult Index()
        {
            var movies = this.Data.Movies.All()
                             .OrderBy(m => m.Name)
                             .Project().To<MovieOutputModel>();

            return this.View(movies);
        }

        public ActionResult Read([DataSourceRequest]
                                 DataSourceRequest request)
        {
            var result = this.Data.Movies.All()
                             .Project().To<MovieOutputModel>();

            return this.Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieInputModel movie, HttpPostedFileBase poster, HttpPostedFileBase banner, HttpPostedFileBase subtitlesource)
        {
            if (subtitlesource == null)
            {
                this.ModelState.AddModelError(string.Empty, "Subtitle source is required, please upload a file.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(movie);
            }

            DbFile dbFile = null;

            try
            {
                dbFile = DatabaseFileHelper.GetDbFile(subtitlesource, "initialSubtitleSource");

                if (poster != null)
                {
                    movie.MainPosterUrl = UploadHelper.UploadPictureToServer(poster, "poster", movie);
                }

                if (banner != null)
                {
                    movie.BannerUrl = UploadHelper.UploadPictureToServer(banner, "banner", movie);
                }
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(movie);
            }

            this.Data.Files.Add(dbFile);

            var dbMovie = Mapper.Map<Movie>(movie);
            dbMovie.InitialSource = dbFile;

            this.Data.Movies.Add(dbMovie);

            this.CreateSubtitlesForAllLanguages(dbMovie);

            this.Data.SaveChanges();

            return this.RedirectToAction("Edit", new { id = dbMovie.Id });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var movie = this.Data.Movies.All().Where(m => m.Id == id).Project().To<MovieInputModel>().FirstOrDefault();

            if (movie == null)
            {
                return this.HttpNotFound();
            }

            return this.View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieInputModel movie, HttpPostedFileBase poster, HttpPostedFileBase banner, HttpPostedFileBase subtitlesource, bool addsubtitles)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(movie);
            }

            var dbMovie = this.Data.Movies.Find(id);

            if (dbMovie == null)
            {
                return this.HttpNotFound();
            }

            movie.Directory = dbMovie.Directory;

            if (!this.ModelState.IsValid)
            {
                return this.View(movie);
            }

            DbFile dbFile = null;

            try
            {
                if (subtitlesource != null)
                {
                    dbFile = DatabaseFileHelper.GetDbFile(subtitlesource, "initialSubtitleSource");
                }

                if (poster != null)
                {
                    movie.MainPosterUrl = UploadHelper.UploadPictureToServer(poster, "poster", movie);
                }

                if (banner != null)
                {
                    movie.BannerUrl = UploadHelper.UploadPictureToServer(banner, "banner", movie);
                }
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(movie);
            }

            if (dbFile != null)
            {
                var currentFile = dbMovie.InitialSource;

                this.Data.Files.Add(dbFile);
                dbMovie.InitialSource = dbFile;

                this.Data.Files.Delete(currentFile);
            }

            dbMovie.Directory = movie.Directory == null ? dbMovie.Directory : movie.Directory;
            dbMovie.BannerUrl = movie.BannerUrl == null ? dbMovie.BannerUrl : movie.BannerUrl;
            dbMovie.MainPosterUrl = movie.MainPosterUrl == null ? dbMovie.MainPosterUrl : movie.MainPosterUrl;

            dbMovie.Description = movie.Description;
            dbMovie.Name = movie.Name;
            dbMovie.ReleaseDate = movie.ReleaseDate;

            this.CreateSubtitlesForAllLanguages(dbMovie);

            this.Data.SaveChanges();

            this.TempData["Success"] = "Movie is updated!";

            return this.RedirectToAction("Edit", new { id = dbMovie.Id });
        }
        
        private void CreateSubtitlesForAllLanguages(Movie movie)
        {
            if (!movie.Subtitles.Any())
            {
                var languages = this.Data.Languages.All();
                foreach (var lang in languages)
                {
                    var sub = new Subtitle()
                    {
                        Description = movie.Description,
                        IsFinished = false,
                        Language = lang,
                        Movie = movie,
                        Name = movie.Name,
                        State = SubtitleState.AwaitingTranslationTeam,                        
                    };

                    this.Data.Subtitles.Add(sub);
                }
            }
            else
            {
                var subtitleLanguageIds = movie.Subtitles.Where(sc => sc.Language != null).Select(s => s.Language.Id);

                var languages = this.Data.Languages.All();
                foreach (var lang in languages)
                {
                    if (subtitleLanguageIds.Contains(lang.Id))
                    {
                        continue;
                    }

                    var sub = new Subtitle()
                    {
                        Description = movie.Description,
                        IsFinished = false,
                        Language = lang,
                        Movie = movie,
                        Name = movie.Name,
                        State = SubtitleState.AwaitingTranslationTeam,
                    };

                    this.Data.Subtitles.Add(sub);
                }
            }
        }
    }
}