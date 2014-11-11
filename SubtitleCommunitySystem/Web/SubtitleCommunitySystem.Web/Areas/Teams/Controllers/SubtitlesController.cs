namespace SubtitleCommunitySystem.Web.Areas.Teams.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;

    using SubtitleCommunitySystem.Web.Areas.Teams.Models;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Controllers;
    using SubtitleCommunitySystem.Data;

    public class SubtitlesController : AuthenticatedUserController
    {
        public SubtitlesController(IApplicationData data)
            : base(data)
        {

        }

        // GET: Teams/Subtitles
        public ActionResult Index([DataSourceRequest] DataSourceRequest request, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userID = this.User.Identity.GetUserId();
            var isInTeam = this.Data.Teams.All()
                .Where(t => t.Id == id)
                .Any(t => t.Members.Any(m => m.Id == userID));

            if (!isInTeam)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var team = this.Data.Teams.Find(id);

            if (team == null)
            {
                return HttpNotFound();
            }

            var subtitles = team.Subtitles.ToDataSourceResult(request).Data;

            return View(subtitles);
        }
    }
}