namespace SubtitleCommunitySystem.Web.Areas.Administration.Controllers
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
    using SubtitleCommunitySystem.Web.Areas.Administration.Models;
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Web.Helpers;

    public class RequestsController : AdminController
    {
        public RequestsController(IApplicationData data)
            : base(data)
        {
        }

        // GET: Administration/Requests
        public ActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            var requests = this.Data.PromotionRequests.All()
                .Where(r => r.RequestState == RequestState.Pending)
                .OrderBy(r => r.DateCreated)
                .Project().To<RequestDetailedModel>()
                .ToDataSourceResult(request).Data;
            
            return this.View(requests);
        }

        public ActionResult Approved([DataSourceRequest] DataSourceRequest request)
        {
            var requests = this.Data.PromotionRequests.All()
                .Where(r => r.RequestState == RequestState.Approved)
                .OrderByDescending(r => r.DateCreated)
                .Project().To<RequestDetailedModel>()
                .ToDataSourceResult(request).Data;

            return this.View("Index", requests);
        }

        public ActionResult Denied([DataSourceRequest] DataSourceRequest request)
        {
            var requests = this.Data.PromotionRequests.All()
                .Where(r => r.RequestState == RequestState.Denied)
                .OrderByDescending(r => r.DateCreated)
                .Project().To<RequestDetailedModel>()
                .ToDataSourceResult(request).Data;

            return this.View("Index", requests);
        }

        public ActionResult ApproveRequest(int id)
        {
            var request = this.Data.PromotionRequests.All().FirstOrDefault(r => r.Id == id);

            var user = this.Data.PromotionRequests.All().Where(r => r.Id == id).Select(r => r.User).FirstOrDefault();

            if (request == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var roleAsString = RoleEnumToStringConverter.FromRequestType(request.Type);

            bool isAlreadyInRole = user.TeamRoles.Any(r => r.Name == roleAsString);

            if (isAlreadyInRole)
            {
                request.RequestState = RequestState.Approved;

                this.Data.SaveChanges();
                return this.RedirectToAction("Index");
            }

            var role = this.Data.TeamRoles.All().FirstOrDefault(r => r.Name == roleAsString);

            if (role == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            user.TeamRoles.Add(role);
            request.RequestState = RequestState.Approved;

            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }

        public ActionResult DenyRequest(int id)
        {
            var request = this.Data.PromotionRequests.Find(id);

            if (request == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            request.RequestState = RequestState.Denied;

            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}