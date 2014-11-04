namespace SubtitleCommunitySystem.Data
{

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using SubtitleCommunitySystem.Data.Repositories;
    using SubtitleCommunitySystem.Model;

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

        public int SaveChanges()
        {
            return this.context.SaveChanges();
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
