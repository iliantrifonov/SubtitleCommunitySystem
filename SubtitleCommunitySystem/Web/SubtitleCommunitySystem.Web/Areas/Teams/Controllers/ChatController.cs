namespace SubtitleCommunitySystem.Web.Areas.Teams.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Web.Areas.Teams.Models.Chat;

    public class ChatController : AuthenticatedUserController
    {
        public ChatController(IApplicationData data) : base(data)
        {
        }

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            if (!this.CurrentUser.Teams.Any(t => t.Id == id))
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var messages = this.Data.Teams.All()
                .Where(t => t.Id == id)
                .Select(t => t.ChatChannel.Messages
                    .Select(m => new MessageOutputModel() 
                    { 
                        Content = m.Content, 
                        UserName = m.User.UserName,
                        DateSent = m.DateSent
                    })
                    .OrderByDescending(z => z.DateSent))
                    .Take(200)
                    .FirstOrDefault();

            this.ViewBag.Id = id;

            this.ViewBag.TeamName = this.Data.Teams.All()
                .Where(t => t.Id == id)
                .Select(t => t.Name).FirstOrDefault();

            return this.View(messages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMessage(string messageBox)
        {
            return this.Json("This is working..");
        }
    }
}