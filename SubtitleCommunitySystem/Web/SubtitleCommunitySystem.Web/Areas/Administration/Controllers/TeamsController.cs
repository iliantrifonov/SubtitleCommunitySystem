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
    using SubtitleCommunitySystem.Web.Infrastructure.Constants;

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

        [HttpGet]
        public ActionResult ManageMembers(int id)
        {
            var team = this.Data.Teams.Find(id);

            if (team == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dBtranslators = team.Members.Where(m => m.TeamRoles.Any(tr => tr.Name == RoleConstants.Translator));

            var translators = Mapper.Map<IEnumerable<UserOutputModel>>(dBtranslators);

            ManageMembersModel model = new ManageMembersModel()
            {
                Id = team.Id,
                Translators = translators
            };

            return View(model);
        }

        public ActionResult RemoveUserFromTeam(int? id, string userId)
        {

            var team = this.Data.Teams.Find(id);

            if (team == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var memberToRemove = team.Members.FirstOrDefault(u => u.Id == userId);
            if (memberToRemove == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);                
            }

            team.Members.Remove(memberToRemove);

            this.Data.SaveChanges();

            return RedirectToAction("ManageMembers", new { id = id });
        }

        public ActionResult AddMember(int id, string role)
        {
            ViewBag.Role = role;
            ViewBag.Id = id;

            var users = this.Data.Users.All()
                .Where(u=> !u.Teams.Any(t=> t.Id == id))
                .Where(usr => usr.TeamRoles.Any(tr => tr.Name == role))
                .Project().To<UserOutputModel>();

            return View(users);
        }

        public ActionResult AddUserToTeam(int id, string userId, string role)
        {
            var team = this.Data.Teams.Find(id);

            if (team == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userToAdd = this.Data.Users.Find(userId);
            if (userToAdd == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!userToAdd.TeamRoles.Any(tr => tr.Name == role))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            team.Members.Add(userToAdd);

            this.Data.SaveChanges();

            return RedirectToAction("ManageMembers", new { id = id });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
