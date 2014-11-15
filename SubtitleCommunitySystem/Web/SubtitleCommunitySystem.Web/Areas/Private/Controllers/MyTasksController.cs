namespace SubtitleCommunitySystem.Web.Areas.Private.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Areas.Private.Models;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Helpers;

    public class MyTasksController : AuthenticatedUserController
    {
        public MyTasksController(IApplicationData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {

            return View();
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
                ModelState.AddModelError("", ex.Message);
            }

            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            task.FinishedPartFile = dbFile;
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}