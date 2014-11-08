namespace SubtitleCommunitySystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Controllers;
    using SubtitleCommunitySystem.Web.Areas.Administration.Models;

    public class TeamsController : AdminController
    {
        public TeamsController(IApplicationData data) : base(data)
        {

        }

        // GET: Administration/Teams
        public ActionResult Index()
        {
            return View(this.Data.Teams.All()
                .Project().To<TeamOutputModel>()
                .ToList());
        }

        // GET: Administration/Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var team = this.Data.Teams.All()
                .Where(t => t.Id == id)
                .Select(TeamViewModel.FromTeam)
                .FirstOrDefault();

            if (team == null)
            {
                return HttpNotFound();
            }

            return View(team);
        }

        // GET: Administration/Teams/Create
        public ActionResult Create()
        {
            var languages = this.Data.Languages.All().Select(l => new SelectListItem() { Text = l.Name, Value = l.Id.ToString() });

            var createTeamModel = new CreateTeamViewModel()
            {
                Languages = languages,
            };

            return View(createTeamModel);
        }

        // POST: Administration/Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTeamViewModel createTeamViewModel)
        {
            if (ModelState.IsValid)
            {
                var language = this.Data.Languages.Find(int.Parse(createTeamViewModel.Team.LanguageId));

                if (language == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                this.Data.Teams.Add(new Team()
                {
                    Language = language,
                    Name = createTeamViewModel.Team.Name
                });

                this.Data.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(createTeamViewModel);
        }

        // GET: Administration/Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var team = this.Data.Teams.All()
                .Where(t => t.Id == id)
                .Project().To<TeamInputModel>()
                .FirstOrDefault();

            if (team == null)
            {
                return HttpNotFound();
            }

            var languages = this.Data.Languages.All().Select(l => new SelectListItem() { Text = l.Name, Value = l.Id.ToString() });

            var createTeamModel = new CreateTeamViewModel()
            {
                Languages = languages,
                Team = team
            };

            return View(createTeamModel);
        }

        // POST: Administration/Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateTeamViewModel createTeamModel)
        {
            var team = createTeamModel.Team;

            if (ModelState.IsValid)
            {
                var dbLanguage = this.Data.Languages.Find(int.Parse(team.LanguageId));
                if (dbLanguage == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var dbTeam = this.Data.Teams.Find(team.Id);

                dbTeam.Name = team.Name;
                dbTeam.Language = dbLanguage;

                this.Data.SaveChanges();
               
                return RedirectToAction("Edit");
            }

            return View(team);
        }

        // GET: Administration/Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var team = this.Data.Teams.All()
                .Where(t => t.Id == id)
                .Select(TeamViewModel.FromTeam)
                .FirstOrDefault();

            if (team == null)
            {
                return HttpNotFound();
            }

            return View(team);
        }

        // POST: Administration/Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = this.Data.Teams.Find(id);

            team.Members.Clear();
            team.Subtitles.Clear();
 
            this.Data.Teams.Delete(team);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
