namespace SubtitleCommunitySystem.Web.Areas.Teams.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using SubtitleCommunitySystem.Web.Controllers;
    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Areas.Teams.Models;

    public class ChatController : AuthenticatedUserController
    {
        public ChatController(IApplicationData data) : base(data)
        {

        }

        // GET: Teams/Chat/id
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chat = this.Data.Channels.Find(id);

            if (chat == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!this.CurrentUser.Teams.Any(t => t.ChatChannel.Id == id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(chat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMessage(string MessageBox)
        {

            return Json("This is working..");
        }
    }
}