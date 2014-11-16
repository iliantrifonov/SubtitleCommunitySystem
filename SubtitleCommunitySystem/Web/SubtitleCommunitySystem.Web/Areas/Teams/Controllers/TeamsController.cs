namespace SubtitleCommunitySystem.Web.Areas.Teams.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Areas.Teams.Models;
    using SubtitleCommunitySystem.Web.Controllers.Base;

    public class TeamsController : AuthenticatedUserController
    {
        public TeamsController(IApplicationData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var teams = this.CurrentUser.Teams
                .OrderBy(t => t.Name)
                .AsQueryable().Project().To<TeamIndexOutputModel>().ToList();

            return this.View(teams);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var teamExists = this.Data.Teams.All().Any(t => t.Id == id);

            if (!teamExists)
            {
                return this.HttpNotFound();
            }

            var teamModel = this.Data.Teams.All()
                .Where(t => t.Id == id)
                .Project().To<TeamDetailedOutputModel>()
                .FirstOrDefault();

            return this.View(teamModel);
        }
    }
}