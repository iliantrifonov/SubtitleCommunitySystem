namespace SubtitleCommunitySystem.Web.Areas.Private.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Helpers;
    using SubtitleCommunitySystem.Web.Areas.Private.Models;
    using SubtitleCommunitySystem.Web.Controllers.Base;

    public class MyRequestsController : AuthenticatedUserController
    {
        public MyRequestsController(IApplicationData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var user = this.CurrentUser;

            var pendingRequests = user.Requests.AsQueryable()
                .Where(r => r.RequestState == RequestState.Pending)
                .OrderByDescending(r => r.DateCreated)
                .Project().To<RequestOutputModel>()
                .ToList();

            var approvedRequests = user.Requests.AsQueryable()
                .Where(r => r.RequestState == RequestState.Approved)
                .OrderByDescending(r => r.DateCreated)
                .Take(5)
                .Project().To<RequestOutputModel>()
                .ToList();

            var deniedRequests = user.Requests.AsQueryable()
                .Where(r => r.RequestState == RequestState.Denied)
                .OrderByDescending(r => r.DateCreated)
                .Take(5)
                .Project().To<RequestOutputModel>()
                .ToList();

            var indexViewModel = new RequestIndexViewModel()
            {
                PendingRequests = pendingRequests,
                ApprovedRequests = approvedRequests,
                DeniedRequests = deniedRequests
            };

            return this.View(indexViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new RequestInputModel()
            {
                Content = "Your message here..",
                Type = RequestType.Translator
            };
            return this.View(model);
        }

        // POST: Administration/Languages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestInputModel model)
        {
            var type = model.Type;

            var hasPending = this.CurrentUser.Requests.Where(r => r.RequestState == RequestState.Pending)
                .Any(req => req.Type == type);

            if (hasPending)
            {
                this.ModelState.AddModelError(string.Empty, "You already have a request of this type pending");
            }

            var modelRole = RoleEnumToStringConverter.FromRequestType(model.Type);

            var hasRoleAlready = this.CurrentUser.TeamRoles.Any(r => r.Name == modelRole);

            if (hasRoleAlready)
            {
                this.ModelState.AddModelError(string.Empty, "You already have that role!");
            }

            if (this.ModelState.IsValid)
            {
                var request = new PromotionRequest()
                {
                    Content = model.Content,
                    DateCreated = DateTime.Now,
                    RequestState = RequestState.Pending,
                    Type = model.Type,
                };

                this.Data.PromotionRequests.Add(request);

                var userId = this.User.Identity.GetUserId();

                var user = this.Data.Users.Find(userId);

                user.Requests.Add(request);

                this.Data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }
    }
}