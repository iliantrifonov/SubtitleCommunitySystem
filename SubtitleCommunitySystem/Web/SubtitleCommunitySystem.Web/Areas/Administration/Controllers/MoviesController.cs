namespace SubtitleCommunitySystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Areas.Administration.Models;
    using SubtitleCommunitySystem.Web.Controllers;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Constants;

    public class MoviesController : AdminController
    {
        public MoviesController(IApplicationData data)
            : base(data)
        {
        }

        public ActionResult DownloadSource(int id)
        {
            var file = this.Data.Movies.Find(id).InitialSource;
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + file.FileName + "\"");
            return File(file.Content, file.ContentType);
        }

        public ActionResult Index()
        {
            var movies = this.Data.Movies.All().OrderBy(m => m.Name).Project().To<MovieOutputModel>();

            return View(movies);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieInputModel movie, HttpPostedFileBase poster, HttpPostedFileBase banner, HttpPostedFileBase subtitlesource)
        {
            if (subtitlesource == null)
            {
                ModelState.AddModelError("", "Subtitle source is required, please upload a file.");
            }

            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            DbFile dbFile = null;

            try
            {
                dbFile = GetDbFile(subtitlesource, "initialSubtitleSource");

                if (poster != null)
                {
                    movie.MainPosterUrl = UploadFileToServer(poster, "poster", movie);
                }

                if (banner != null)
                {
                    movie.BannerUrl = UploadFileToServer(banner, "banner", movie);
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            this.Data.Files.Add(dbFile);

            var dbMovie = Mapper.Map<Movie>(movie);
            dbMovie.InitialSource = dbFile;


            this.Data.Movies.Add(dbMovie);

            CreateSubtitlesForAllLanguages(dbMovie);

            this.Data.SaveChanges();

            return RedirectToAction("Edit", new { id = dbMovie.Id });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var movie = this.Data.Movies.All().Where(m => m.Id == id).Project().To<MovieInputModel>().FirstOrDefault();

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieInputModel movie, HttpPostedFileBase poster, HttpPostedFileBase banner, HttpPostedFileBase subtitlesource, bool addsubtitles)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            var dbMovie = this.Data.Movies.Find(id);

            if (dbMovie == null)
            {
                return HttpNotFound();
            }

            movie.Directory = dbMovie.Directory;

            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            DbFile dbFile = null;

            try
            {
                if (subtitlesource != null)
                {
                    dbFile = GetDbFile(subtitlesource, "initialSubtitleSource");
                }

                if (poster != null)
                {
                    movie.MainPosterUrl = UploadFileToServer(poster, "poster", movie);
                }

                if (banner != null)
                {
                    movie.BannerUrl = UploadFileToServer(banner, "banner", movie);
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            if (!ModelState.IsValid)
            {
                return View(movie);
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

            CreateSubtitlesForAllLanguages(dbMovie);

            this.Data.SaveChanges();

            return RedirectToAction("Edit", new { id = dbMovie.Id });
        }

        private string UploadFileToServer(HttpPostedFileBase file, string fileName, MovieInputModel movie)
        {
            var extention = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            if (!FileConstants.AllowedPictureExtentions.Contains(extention))
            {
                throw new ArgumentException("Incorrect file extention type.");
            }

            var directoryName = "~/Files/" + movie.Name;
            if (string.IsNullOrWhiteSpace(movie.Directory))
            {
                if (Directory.Exists(Server.MapPath("~/Files/" + movie.Name)))
                {
                    var i = 0;
                    while (Directory.Exists(Server.MapPath("~/Files/" + movie.Name + "." + i)))
                    {
                        i++;
                    }

                    directoryName = "~/Files/" + movie.Name + "." + i;
                }
            }
            else
            {
                directoryName = movie.Directory;
            }


            movie.Directory = directoryName;
            var mappedDirectoryName = Server.MapPath(directoryName);
            Directory.CreateDirectory(mappedDirectoryName);

            file.SaveAs(mappedDirectoryName + "/" + fileName + extention);

            return directoryName.Substring(1) + "/" + fileName + extention;
        }

        private DbFile GetDbFile(HttpPostedFileBase file, string fileName)
        {
            var extention = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            if (!FileConstants.AllowedSubtitleExtentions.Contains(extention))
            {
                throw new ArgumentException("Incorrect file extention type.");
            }

            var dbFile = new DbFile()
            {
                ContentType = file.ContentType,
                FileName = fileName + extention,
            };

            using (var memoryStream = new MemoryStream())
            {
                file.InputStream.CopyTo(memoryStream);
                dbFile.Content = memoryStream.ToArray();
            }

            return dbFile;
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
                var subtitleLanguageIds = movie.Subtitles.Select(s => s.Language.Id);

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