namespace SubtitleCommunitySystem.Data
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;

    using SubtitleCommunitySystem.Data.Repositories;
    using SubtitleCommunitySystem.Model;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    public class ApplicationData : IApplicationData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public ApplicationData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public ApplicationData()
            : this(new ApplicationDbContext())
        {
        }

        public IRepository<TeamRole> TeamRoles
        {
            get
            {
                return GetRepository<TeamRole>();
            }
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return GetRepository<ApplicationUser>();
            }
        }

        public IRepository<Language> Languages
        {
            get
            {
                return GetRepository<Language>();
            }
        }

        public IRepository<Channel> Channels
        {
            get
            {
                return GetRepository<Channel>();
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                return GetRepository<Message>();
            }
        }

        public IRepository<Subtitle> Subtitles
        {
            get
            {
                return GetRepository<Subtitle>();
            }
        }

        public IRepository<Team> Teams
        {
            get
            {
                return GetRepository<Team>();
            }
        }

        public IRepository<DbFile> Files
        {
            get
            {
                return GetRepository<DbFile>();
            }
        }

        public IRepository<SubtitleTask> Tasks
        {
            get
            {
                return GetRepository<SubtitleTask>();
            }
        }

        public IRepository<Movie> Movies
        {
            get
            {
                return GetRepository<Movie>();
            }
        }

        public IRepository<PromotionRequest> PromotionRequests
        {
            get
            {
                return GetRepository<PromotionRequest>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public bool AddRoleToUser(ApplicationUser user, string role)
        {
            try
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.context));
                userManager.AddToRole(user.Id, role);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool RemoveRoleFromUser(ApplicationUser user, string roleName)
        {
            var identityContext = context as IdentityDbContext<ApplicationUser>;
            var role = identityContext.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                return false;
            }

            var roleId = role.Id;
            try
            {
                var roleInUser = user.Roles.FirstOrDefault(r => r.RoleId == roleId);
                user.Roles.Remove(roleInUser);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }
            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
