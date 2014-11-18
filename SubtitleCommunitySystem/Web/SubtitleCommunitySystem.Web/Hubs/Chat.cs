namespace SubtitleCommunitySystem.Web.Hubs
{
    using System;
    using System.Web;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;

    public class Chat : Hub
    {
        private IApplicationData data;

        public Chat()
        {
            this.data = new ApplicationData();
        }

        public void JoinRoom(string room)
        {
            if (room == null)
            {
                throw new HttpException(400, "Invalid TeamId");
            }

            var userId = this.Context.User.Identity.GetUserId();

            var teamId = int.Parse(room);

            var teamContainsUser = this.data.Teams.All().Where(t => t.Id == teamId).Any(t => t.Members.Any(m => m.Id == userId));

            if (!teamContainsUser)
            {
                throw new HttpException(400, "You are not a part of this team, or the team does not exist");
            }

            this.Groups.Add(this.Context.ConnectionId, room);
            this.Clients.Caller.joinRoom(room);
        }

        public void SendMessageToRoom(string message, string[] rooms)
        {
            var teamIdToString = rooms.FirstOrDefault();

            if (teamIdToString == null)
            {
                throw new HttpException(400, "Invalid TeamId");
            }

            int teamId = int.Parse(teamIdToString);

            var userId = this.Context.User.Identity.GetUserId();

            var teamContainsUser = this.data.Teams.All().Where(t => t.Id == teamId).Any(t => t.Members.Any(m => m.Id == userId));

            if (!teamContainsUser)
            {
                throw new HttpException(400, "You are not a part of this team, or the team does not exist");
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new HttpException(400, "Message cannot be null or empty");
            }

            if (message.Length > 999)
            {
                throw new HttpException(400, "Message too long");
            }

            var escapedMessage = HttpUtility.HtmlEncode(message);

            var dbMessage = new Message()
            {
                Content = escapedMessage,
                DateSent = DateTime.Now,
                UserId = userId
            };

            this.data.Messages.Add(dbMessage);

            var chatChan = this.data.Teams.All()
                .Where(t => t.Id == teamId)
                .Select(t => t.ChatChannel)
                .FirstOrDefault();

            chatChan.Messages.Add(dbMessage);

            this.data.SaveChanges();

            var userName = this.Context.User.Identity.GetUserName();

            for (int i = 0; i < rooms.Length; i++)
            {
                this.Clients.Group(rooms[i]).addMessage(userName, dbMessage.Content);
            }
        }
    }
}