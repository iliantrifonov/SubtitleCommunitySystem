namespace SubtitleCommunitySystem.Web.Areas.Private.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Areas.Private.Models;
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Web.Helpers;

    public class MyTasksController : AuthenticatedUserController
    {
        public MyTasksController(IApplicationData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult ReadTasks([DataSourceRequest]DataSourceRequest request)
        {
            var id = this.CurrentUser.Id;
            var items = this.Data.Tasks.All()
                .Where(c => c.User.Id == id)
                .Project().To<TaskOutputModel>()
                .ToDataSourceResult(request);

            return this.Json(items);
        }

        [HttpPost]
        public ActionResult Upload(int? id, HttpPostedFileBase file)
        {
            if (file == null)
            {
                throw new HttpException(400, "File not present or valid");
            }

            var task = this.Data.Tasks.Find(id);

            if (task == null)
            {
                throw new HttpException(404, "Task not found");
            }

            DbFile dbFile = null;

            try
            {
                if (file != null)
                {
                    dbFile = DatabaseFileHelper.GetSubtitleDbFile(file, "Subtitles");
                }
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View("Index");
            }

            task.FinishedPartFile = dbFile;
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdatePercent(int? id, int? percent)
        {
            if (id == null || percent == null)
            {
                throw new HttpException(400, "Missing task or percent when updating percent!");
            }

            var task = this.Data.Tasks.Find(id);

            if (task == null)
            {
                throw new HttpException(404, "No such task");
            }

            if (percent < 0 || percent > 100)
            {
                throw new HttpException(400, "Percent value is invalid!");
            }

            task.PercentDone = (int)percent;
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}