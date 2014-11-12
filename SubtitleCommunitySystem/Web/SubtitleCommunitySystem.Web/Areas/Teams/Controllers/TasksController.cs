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
    using SubtitleCommunitySystem.Web.Controllers.Base;


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
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model, ViewModel>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.Data.Tasks.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}