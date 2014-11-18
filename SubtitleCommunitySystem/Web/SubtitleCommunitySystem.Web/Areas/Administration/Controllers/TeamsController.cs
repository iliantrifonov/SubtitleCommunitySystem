namespace SubtitleCommunitySystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Areas.Administration.Models;
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Web.Infrastructure.Constants;

    public class TeamsController : AdminController
    {
        public TeamsController(IApplicationData data) : base(data)
        {
        }

        // GET: Administration/Teams
        public ActionResult Index([DataSourceRequest]
                                  DataSourceRequest request)
        {
            return this.View(this.Data.Teams.All().Project().To<TeamOutputModel>().ToDataSourceResult(request).Data);
        }

        // GET: Administration/Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var team = this.Data.Teams.All()
                           .Where(t => t.Id == id)
                           .Select(TeamViewModel.FromTeam)
                           .FirstOrDefault();

            if (team == null)
            {
                return this.HttpNotFound();
            }

            return this.View(team);
        }

        // GET: Administration/Teams/Create
        public ActionResult Create()
        {
            var languages = this.Data.Languages.All()
                                .OrderBy(l => l.Name)
                                .Select(l => new SelectListItem() { Text = l.Name, Value = l.Id.ToString() });

            var createTeamModel = new CreateTeamViewModel()
            {
                Languages = languages,
            };

            return this.View(createTeamModel);
        }

        // POST: Administration/Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTeamViewModel createTeamViewModel)
        {
            if (this.ModelState.IsValid)
            {
                var teamNameExists = this.Data.Teams.All().Any(c => c.Name == createTeamViewModel.Team.Name);
                if (teamNameExists)
                {
                    this.ModelState.AddModelError(string.Empty, "Team name already exists!");
                }
            }

            if (this.ModelState.IsValid)
            {
                var language = this.Data.Languages.Find(int.Parse(createTeamViewModel.Team.LanguageId));

                if (language == null)
                {
                    throw new HttpException(404, "Incorrect input data!");
                }

                var team = new Team()
                {
                    Language = language,
                    Name = createTeamViewModel.Team.Name
                };

                this.Data.Teams.Add(team);

                var chatChannel = new Channel()
                {
                    Name = team.Name,
                };

                chatChannel.Teams.Add(team);
                this.Data.Channels.Add(chatChannel);

                this.Data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(createTeamViewModel);
        }

        // GET: Administration/Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var team = this.Data.Teams.All()
                           .Where(t => t.Id == id)
                           .Project().To<TeamInputModel>()
                           .FirstOrDefault();

            if (team == null)
            {
                return this.HttpNotFound();
            }

            var languages = this.Data.Languages.All()
                                .OrderBy(l => l.Name)
                                .Select(l => new SelectListItem() { Text = l.Name, Value = l.Id.ToString() });

            var createTeamModel = new CreateTeamViewModel()
            {
                Languages = languages,
                Team = team
            };

            return this.View(createTeamModel);
        }

        // POST: Administration/Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateTeamViewModel createTeamModel)
        {
            var team = createTeamModel.Team;

            if (this.ModelState.IsValid)
            {
                var dbLanguage = this.Data.Languages.Find(int.Parse(team.LanguageId));
                if (dbLanguage == null)
                {
                    throw new HttpException(404, "Incorrect input data!");
                }

                var dbTeam = this.Data.Teams.Find(team.Id);

                dbTeam.Name = team.Name;
                dbTeam.Language = dbLanguage;

                this.Data.SaveChanges();
                this.TempData["Success"] = "Team is updated!";

                return this.RedirectToAction("Edit");
            }

            return this.View(team);
        }

        // GET: Administration/Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var team = this.Data.Teams.All()
                           .Where(t => t.Id == id)
                           .Select(TeamViewModel.FromTeam)
                           .FirstOrDefault();

            if (team == null)
            {
                return this.HttpNotFound();
            }

            return this.View(team);
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
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ManageMembers(int id)
        {
            var team = this.Data.Teams.Find(id);

            if (team == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var dBtranslators = team.Members.Where(m => m.TeamRoles.Any(tr => tr.Name == RoleConstants.Translator));
            var translators = Mapper.Map<IEnumerable<UserOutputModel>>(dBtranslators);

            var dBImageManagers = team.Members.Where(m => m.TeamRoles.Any(tr => tr.Name == RoleConstants.ImageManager));
            var imageManagers = Mapper.Map<IEnumerable<UserOutputModel>>(dBImageManagers);

            var dbSyncs = team.Members.Where(m => m.TeamRoles.Any(tr => tr.Name == RoleConstants.Sync));
            var syncs = Mapper.Map<IEnumerable<UserOutputModel>>(dbSyncs);

            var dBRevisioners = team.Members.Where(m => m.TeamRoles.Any(tr => tr.Name == RoleConstants.Revisioner));
            var revisioners = Mapper.Map<IEnumerable<UserOutputModel>>(dBRevisioners);

            var dBTeamLeaders = team.Members.Where(m => m.TeamRoles.Any(tr => tr.Name == RoleConstants.TeamLeader));
            var teamLeaders = Mapper.Map<IEnumerable<UserOutputModel>>(dBTeamLeaders);

            ManageMembersModel model = new ManageMembersModel()
            {
                Id = team.Id,
                Translators = translators,
                ImageManagers = imageManagers,
                Syncs = syncs,
                Revisioners = revisioners,
                TeamLeaders = teamLeaders
            };

            return this.View(model);
        }

        public ActionResult RemoveUserFromTeam(int? id, string userId)
        {
            var team = this.Data.Teams.Find(id);

            if (team == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var memberToRemove = team.Members.FirstOrDefault(u => u.Id == userId);
            if (memberToRemove == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            team.Members.Remove(memberToRemove);

            this.Data.SaveChanges();

            return this.RedirectToAction("ManageMembers", new { id = id });
        }

        public ActionResult AddMember(int id, string role)
        {
            this.ViewBag.Role = role;
            this.ViewBag.Id = id;

            var team = this.Data.Teams.Find(id);

            if (team == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var users = this.Data.Users.All()
                            .Where(us => us.Languages.Any(l => l.Id == team.Language.Id))
                            .Where(u => !u.Teams.Any(t => t.Id == id))
                            .Where(usr => usr.TeamRoles.Any(tr => tr.Name == role))
                            .OrderBy(u => u.UserName)
                            .Project().To<UserOutputModel>();

            return this.View(users);
        }

        public ActionResult AddUserToTeam(int id, string userId, string role)
        {
            var team = this.Data.Teams.Find(id);

            if (team == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var userToAdd = this.Data.Users.Find(userId);
            if (userToAdd == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            if (!userToAdd.TeamRoles.Any(tr => tr.Name == role))
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            team.Members.Add(userToAdd);

            this.Data.SaveChanges();

            return this.RedirectToAction("ManageMembers", new { id = id });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
