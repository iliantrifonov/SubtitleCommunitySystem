﻿using SubtitleCommunitySystem.Data;
using SubtitleCommunitySystem.Model;
using SubtitleCommunitySystem.Web.Areas.Administration.Models;
using SubtitleCommunitySystem.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SubtitleCommunitySystem.Web.Areas.Administration.Controllers
{
    public class MoviesController : AdminController
    {
        public MoviesController(IApplicationData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            var movies = this.Data.Movies.All().OrderBy(m => m.Name).Select(m => new MovieOutputModel() 
            {  
                BannerUrl = m.BannerUrl,
                Description = m.Description,
                Id = m.Id,
                MainPosterUrl = m.MainPosterUrl,
                Name = m.Name
            });

            return View(movies);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie, HttpPostedFileBase poster, HttpPostedFileBase banner)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            try
            {
                if (poster != null)
                {
                    movie.MainPosterUrl = UploadFile(poster, "poster", movie);
                }

                if (banner != null)
                {
                    movie.BannerUrl = UploadFile(banner, "banner", movie);
                }
            }
            catch (ArgumentException ex)
            {
                return Content(ex.Message);
            }

            this.Data.Movies.Add(movie);

            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }

        private string UploadFile(HttpPostedFileBase file, string fileName, Movie movie)
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
    }
}