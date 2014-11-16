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
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Helpers;
    using SubtitleCommunitySystem.Web.Filters;
    using SubtitleCommunitySystem.Web.Infrastructure.Constants;

    public class SubtitlesController : AuthenticatedUserController
    {
        public SubtitlesController(IApplicationData data)
            : base(data)
        {

        }

        public ActionResult Index([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var error = GetErrorValidateTeamAndUser(id);

            if (error != null)
            {
                return error;
            }

            SetViewBag(id);


            int languageId = this.Data.Teams.All()
                                 .Where(t => t.Id == id)
                                 .Select(t => t.Language.Id).FirstOrDefault();

            var subtitles = this.Data.Subtitles.All()
                                .Where(s => s.Language.Id == languageId)
                                .Where(s => s.IsFinished == false)
                                .Where(s => s.State == SubtitleState.AwaitingTranslationTeam || s.Team == null)
                                .Project().To<SubtitleOutputModel>()
                                .ToDataSourceResult(request);

            return View(subtitles.Data);
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
                return HttpNotFound();
            }

            subtitle.Team = team;
            subtitle.State = SubtitleState.InTranslation;

            this.Data.SaveChanges();

            return RedirectToAction("Index", new { id = teamId });
        }

        public ActionResult TeamSubtitles([DataSourceRequest] DataSourceRequest request, int? id)
        {
            SetViewBag(id);

            return View();
        }

        public ActionResult ReadTeamSubtitles([DataSourceRequest] DataSourceRequest request, int? id)
        {

            var errorResult = GetErrorValidateTeamAndUser(id);

            if (errorResult != null)
            {
                return errorResult;
            }



            var subtitles = this.Data.Subtitles.All()
                .Where(s => s.Team.Id == id)
                .Project().To<SubtitleOutputModel>()
                .ToDataSourceResult(request);

            return Json(subtitles);
        }

        public ActionResult Details(int? id, int? teamId)
        {
            var errorResult = GetErrorValidateTeamAndUser(teamId);

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

            ViewBag.Users = selectList;

            SetViewBag(teamId);

            return View(model);
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
                ModelState.AddModelError("", ex.Message);
            }

            if (!ModelState.IsValid)
            {
                return View("Details", subtitle);
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
                    TempData["Error"] = "Cannot finish a subtitle without uploading the final subtitle file!";
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

            return RedirectToAction("Details", new { id = subtitle.Id, teamId = subtitle.Team.Id });
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
                ViewBag.TeamName = team.Name;
                ViewBag.Id = team.Id;
            }
        }
    }
}