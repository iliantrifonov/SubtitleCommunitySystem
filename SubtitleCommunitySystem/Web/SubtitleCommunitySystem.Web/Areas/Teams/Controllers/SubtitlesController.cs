namespace SubtitleCommunitySystem.Web.Areas.Teams.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;
    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Areas.Teams.Models;
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Web.Filters;
    using SubtitleCommunitySystem.Web.Helpers;
    using SubtitleCommunitySystem.Web.Infrastructure.Constants;

    public class SubtitlesController : AuthenticatedUserController
    {
        public SubtitlesController(IApplicationData data) : base(data)
        {
        }

        public ActionResult Index([DataSourceRequest]
                                  DataSourceRequest request, int? id)
        {
            var error = this.GetErrorValidateTeamAndUser(id);

            if (error != null)
            {
                return error;
            }

            this.SetViewBag(id);

            int languageId = this.Data.Teams.All()
                                 .Where(t => t.Id == id)
                                 .Select(t => t.Language.Id).FirstOrDefault();

            var subtitles = this.Data.Subtitles.All()
                                .Where(s => s.Language.Id == languageId)
                                .Where(s => s.IsFinished == false)
                                .Where(s => s.State == SubtitleState.AwaitingTranslationTeam || s.Team == null)
                                .Project().To<SubtitleOutputModel>()
                                .ToDataSourceResult(request);

            return this.View(subtitles.Data);
        }

        [Auth(RoleConstants.TeamLeader)]
        public ActionResult AddSubtitleToTeam(int? id, int? teamId)
        {
            if (id == null || teamId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var subtitle = this.Data.Subtitles.Find(id);
            var team = this.Data.Teams.Find(teamId);

            if (subtitle == null || team == null)
            {
                return this.HttpNotFound();
            }

            subtitle.Team = team;
            subtitle.State = SubtitleState.InTranslation;

            this.Data.SaveChanges();

            return this.RedirectToAction("Index", new { id = teamId });
        }

        public ActionResult TeamSubtitles([DataSourceRequest]
                                          DataSourceRequest request, int? id)
        {
            this.SetViewBag(id);

            return this.View();
        }

        public ActionResult ReadTeamSubtitles([DataSourceRequest]
                                              DataSourceRequest request, int? id)
        {
            var errorResult = this.GetErrorValidateTeamAndUser(id);

            if (errorResult != null)
            {
                return errorResult;
            }

            var subtitles = this.Data.Subtitles.All()
                                .Where(s => s.Team.Id == id)
                                .Project().To<SubtitleOutputModel>()
                                .ToDataSourceResult(request);

            return this.Json(subtitles);
        }

        public ActionResult Details(int? id, int? teamId)
        {
            var errorResult = this.GetErrorValidateTeamAndUser(teamId);

            if (errorResult != null)
            {
                return errorResult;
            }

            var model = this.Data.Subtitles.All()
                            .Where(s => s.Id == id)
                            .Project().To<SubtitleDetailsModel>().FirstOrDefault();

            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var selectList = this.Data.Teams.Find(teamId).Members.Select(m => new SelectListItem()
            {
                Text = m.UserName,
                Value = m.Id
            }).ToList();

            this.ViewBag.Users = selectList;

            this.SetViewBag(teamId);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, SubtitleDetailsModel model, HttpPostedFileBase descriptionFile, HttpPostedFileBase subtitleFile)
        {
            var subtitle = this.Data.Subtitles.Find(id);

            if (subtitle == null)
            {
                throw new HttpException(404, "Subtitle not found!");
            }

            DbFile dbFile = null;
            DbFile dbSubtitleFile = null;

            try
            {
                if (descriptionFile != null)
                {
                    dbFile = DatabaseFileHelper.GetDbFile(descriptionFile, "TaskDescription");
                }

                if (subtitleFile != null)
                {
                    dbSubtitleFile = DatabaseFileHelper.GetSubtitleDbFile(subtitleFile, "Subtitles");
                }
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View("Details", subtitle);
            }

            subtitle.Description = model.Description;
            subtitle.Name = model.Name;
            subtitle.IsFinished = model.IsFinished;

            if (dbFile != null)
            {
                subtitle.PartialFile = dbFile;
            }

            if (dbSubtitleFile != null)
            {
                subtitle.FinalFile = dbSubtitleFile;
            }

            if (subtitle.IsFinished)
            {
                if (subtitle.FinalFile == null)
                {
                    subtitle.IsFinished = false;
                    this.TempData["Error"] = "Cannot finish a subtitle without uploading the final subtitle file!";
                }
                else
                {
                    subtitle.DateCompleted = DateTime.Now;
                }
            }

            if (!subtitle.IsFinished)
            {
                subtitle.DateCompleted = null;
            }

            this.Data.SaveChanges();

            return this.RedirectToAction("Details", new { id = subtitle.Id, teamId = subtitle.Team.Id });
        }

        private ActionResult GetErrorValidateTeamAndUser(int? id)
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

            return null;
        }

        private void SetViewBag(int? teamId)
        {
            var team = this.Data.Teams.Find(teamId);

            if (team != null)
            {
                this.ViewBag.TeamName = team.Name;
                this.ViewBag.Id = team.Id;
            }
        }
    }
}