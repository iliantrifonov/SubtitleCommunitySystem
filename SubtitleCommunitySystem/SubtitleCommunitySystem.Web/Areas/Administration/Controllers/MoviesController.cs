using SubtitleCommunitySystem.Data;
using SubtitleCommunitySystem.Model;
using SubtitleCommunitySystem.Web.Areas.Administration.Models;
using SubtitleCommunitySystem.Web.Controllers;
using System;
using System.Collections.Generic;
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
            var movies = this.Data.Movies.All().OrderBy(m => m.Name).Select(m => new MovieOutputModel() {  Name = m.Name });
            return View(movies);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            this.Data.Movies.Add(movie);

            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}