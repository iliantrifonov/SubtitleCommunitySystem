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
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Web.Areas.Administration.Models;

    public class UsersController : AdminController
    {
        public UsersController(IApplicationData data)
            : base(data)
        {

        }

        // GET: Administration/Users
        public ActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            return View(this.Data.Users.All()
                .Project().To<UserOutputModel>()
                .ToDataSourceResult(request).Data);
        }

        public ActionResult Details(string id)
        {
            return RedirectToAction("Edit", new { id = id });
        }

        // GET: Administration/Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.Data.Users.All()
                .Where(u => u.Id == id)
                .Select(UserInputModel.ToModel).FirstOrDefault();

            if (user == null)
            {
                return HttpNotFound();
            }

            var teamRoles = this.Data.TeamRoles.All().Project().To<TeamRoleModel>().ToArray();

            var editUserViewModel = new EditUserViewModel()
            {
                TeamRoles = teamRoles,
                User = user
            };

            return View(editUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserViewModel editModel, IEnumerable<int> selectedGroups)
        {
            if (editModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.Data.Users.Find(editModel.User.Id);

            if (user == null)
            {
                return HttpNotFound();
            }

            user.UserName = editModel.User.UserName;

            user.TeamRoles.Clear();

            if (selectedGroups != null)
            {
                foreach (var role in selectedGroups)
                {
                    var dbRole = this.Data.TeamRoles.Find(role);
                    if (dbRole == null)
                    {
                        continue;
                    }

                    user.TeamRoles.Add(dbRole);
                }
            }

            this.Data.SaveChanges();

            TempData["Success"] = "User is updated!";

            return RedirectToAction("Edit");
        }


        // GET: Administration/Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser applicationUser = this.Data.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<UserOutputModel>(applicationUser));
        }

        // POST: Administration/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = this.Data.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            applicationUser.Messages.Clear();
            applicationUser.Requests.Clear();
            applicationUser.Tasks.Clear();
            applicationUser.TeamRoles.Clear();
            applicationUser.Teams.Clear();

            this.Data.Users.Delete(applicationUser);
            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
