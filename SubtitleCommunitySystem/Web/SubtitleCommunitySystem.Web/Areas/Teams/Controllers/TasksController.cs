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
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Filters;
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Web.Infrastructure.Constants;
    using SubtitleCommunitySystem.Web.Helpers;


    using Model = SubtitleCommunitySystem.Model.SubtitleTask;
    using ViewModel = SubtitleCommunitySystem.Web.Areas.Teams.Models.TaskInputModel;
    using SubtitleCommunitySystem.Web.Areas.Teams.Models;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Services;

    public class TasksController : KendoGridController
    {
        private ICacheService cacheService;

        public TasksController(IApplicationData data, ICacheService cacheService) : base(data)
        {
            this.cacheService = cacheService;
        }

        [HttpPost]
        public ActionResult ReadTasks([DataSourceRequest]DataSourceRequest request, int? subtitleId)
        {
            var items =
            this.GetData(subtitleId)
            .ToDataSourceResult(request);
            return this.Json(items);
        }

        protected IEnumerable GetData(int? subtitleId)
        {
            var data = this.Data.Tasks.All()
                .Where(t => t.SubtitleId == subtitleId)
                .Project().To<ViewModel>();

            return data;
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
            model.DateCreated = DateTime.Now;
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
            if (model != null)
            {
                try
                {
                    this.Data.Tasks.Delete(model.Id);
                }
                catch (ArgumentNullException ex)
                {
                    // this sometimes happens with db data before latest migration.
                }

                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        [HttpGet]
        public ActionResult GetCascadeUsers(int? typeId, int? teamId)
        {
            SubtitleTaskType type = (SubtitleTaskType)typeId;

            var roleName = RoleEnumToStringConverter.FromSubtitleTaskType(type);

            var userModels = cacheService.GetDropDownForUsers(roleName, teamId);

            return Json(userModels, JsonRequestBehavior.AllowGet);
        }

        //public IEnumerable DropDownService(string roleName, int? teamId)
        //{
        //    return this.Data.Users.All()
        //        .Where(u => u.Teams.Any(t => t.Id == teamId))
        //        .Where(u => u.TeamRoles.Any(r => r.Name == roleName))
        //        .Project().To<UserDropDownModel>();
        //}
    }
}