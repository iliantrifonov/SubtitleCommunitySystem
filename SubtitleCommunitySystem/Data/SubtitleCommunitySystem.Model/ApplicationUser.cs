namespace SubtitleCommunitySystem.Model
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.TeamRoles = new HashSet<TeamRole>();
            this.Messages = new HashSet<Message>();
            this.Teams = new HashSet<Team>();
            this.Tasks = new HashSet<SubtitleTask>();
            this.Requests = new HashSet<PromotionRequest>();
            this.Languages = new HashSet<Language>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<TeamRole> TeamRoles { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<SubtitleTask> Tasks { get; set; }

        public virtual ICollection<PromotionRequest> Requests { get; set; }

        public virtual ICollection<Language> Languages { get; set; }
    }
}
