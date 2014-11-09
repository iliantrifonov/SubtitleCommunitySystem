namespace SubtitleCommunitySystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Controllers;
    using SubtitleCommunitySystem.Web.Areas.Administration.Models;
    using SubtitleCommunitySystem.Web.Helpers;

    public class RequestsController : AdminController
    {
        public RequestsController(IApplicationData data)
            : base(data)
        {

        }

        // GET: Administration/Requests
        public ActionResult Index()
        {
            var requests = this.Data.PromotionRequests.All()
                .Where(r => r.RequestState == RequestState.Pending)
                .OrderBy(r => r.DateCreated)
                .Project().To<RequestDetailedModel>().ToList();
            
            return View(requests);
        }

        public ActionResult Approved()
        {
            var requests = this.Data.PromotionRequests.All()
                .Where(r => r.RequestState == RequestState.Approved)
                .OrderByDescending(r => r.DateCreated)
                .Project().To<RequestDetailedModel>().ToList();

            return View("Index", requests);
        }

        public ActionResult Denied()
        {
            var requests = this.Data.PromotionRequests.All()
                .Where(r => r.RequestState == RequestState.Denied)
                .OrderByDescending(r => r.DateCreated)
                .Project().To<RequestDetailedModel>().ToList();

            return View("Index", requests);
        }

        public ActionResult ApproveRequest(int id)
        {
            var request = this.Data.PromotionRequests.All().FirstOrDefault(r => r.Id == id);

            var user = this.Data.PromotionRequests.All().Where(r => r.Id == id).Select(r => r.User).FirstOrDefault();

            if (request == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var roleAsString = RoleEnumToStringConverter.GetRole(request.Type);

            bool isAlreadyInRole = user.TeamRoles.Any(r => r.Name == roleAsString);

            if (isAlreadyInRole)
            {
                request.RequestState = RequestState.Approved;

                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            var role = this.Data.TeamRoles.All().FirstOrDefault(r => r.Name == roleAsString);

            if (role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            user.TeamRoles.Add(role);
            request.RequestState = RequestState.Approved;

            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DenyRequest(int id)
        {
            var request = this.Data.PromotionRequests.Find(id);

            if (request == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            request.RequestState = RequestState.Denied;

            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}