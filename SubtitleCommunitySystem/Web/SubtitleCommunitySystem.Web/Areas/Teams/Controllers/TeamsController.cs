namespace SubtitleCommunitySystem.Web.Areas.Teams.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Areas.Teams.Models;

    public class TeamsController : AuthenticatedUserController
    {
        public TeamsController(IApplicationData data)
            : base(data)
        {

        }

        // GET: Teams/Teams
        public ActionResult Index()
        {
            var teams = this.CurrentUser.Teams
                .OrderBy(t => t.Name)
                .AsQueryable().Project().To<TeamIndexOutputModel>().ToList();

            return View(teams);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var teamExists = this.Data.Teams.All().Any(t => t.Id == id);

            if (!teamExists)
            {
                return HttpNotFound();
            }

            var teamModel = this.Data.Teams.All()
                .Where(t => t.Id == id)
                .Project().To<TeamDetailedOutputModel>()
                .FirstOrDefault();

            return View(teamModel);
        }
    }
}