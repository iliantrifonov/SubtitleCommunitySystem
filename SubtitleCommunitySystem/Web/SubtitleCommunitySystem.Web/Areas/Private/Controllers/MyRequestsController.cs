namespace SubtitleCommunitySystem.Web.Areas.Private.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Web.Areas.Private.Models;
    using SubtitleCommunitySystem.Model;

    public class MyRequestsController : AuthenticatedUserController
    {
        public MyRequestsController(IApplicationData data)
            : base(data)
        {

        }

        // GET: Private/MyRequests
        public ActionResult Index()
        {
            var user = CurrentUser;

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

            return View(indexViewModel);
        }

        public ActionResult Create()
        {
            var model = new RequestInputModel()
            {
                Content = "Your message here..",
                Type = RequestType.Translator
            };
            return View(model);
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
                ModelState.AddModelError("", "You already have a request of this type pending");
            }

            if (ModelState.IsValid)
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

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}