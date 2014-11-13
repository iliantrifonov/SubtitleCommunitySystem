namespace SubtitleCommunitySystem.Web.Areas.Teams.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Kendo.Mvc.UI;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Filters;
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Web.Infrastructure.Constants;


    using Model = SubtitleCommunitySystem.Model.SubtitleTask;
    using ViewModel = SubtitleCommunitySystem.Web.Areas.Teams.Models.TaskInputModel;

    public class TasksController : KendoGridController
    {
        public TasksController(IApplicationData data) : base(data)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            var data = this.Data.Tasks.All()
                .Project().To<ViewModel>();
            return data;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Tasks.Find(id) as T;
        }

        [HttpPost]
        [Auth(RoleConstants.TeamLeader)]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model, int? subId)
        {
            model.SubtitleId = subId;
            var dbModel = base.Create<Model, ViewModel>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        [Auth(RoleConstants.TeamLeader)]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model, int? subId)
        {
            model.SubtitleId = subId;
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        [Auth(RoleConstants.TeamLeader)]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                //this.Data.Tasks.Delete(model.Id);
                var task = this.Data.Tasks.Find(model.Id);

                task.Subtitle = null;
                task.FinishedPartFile = null;
                task.Subtitle = null;
                task.SubtitleId = null;
                task.User = null;

                this.Data.Tasks.Delete(task);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}