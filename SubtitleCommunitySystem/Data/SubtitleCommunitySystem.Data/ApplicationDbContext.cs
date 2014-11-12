﻿namespace SubtitleCommunitySystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity.EntityFramework;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Data.Migrations;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        //public IDbSet<TestModelOne> Ones { get; set; }

        public IDbSet<TeamRole> TeamRoles { get; set; }

        public IDbSet<Language> Languages { get; set; }

        public IDbSet<Subtitle> Subtitles { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Channel> Channels { get; set; }

        public IDbSet<Team> Teams { get; set; }

        public IDbSet<DbFile> Files { get; set; }

        public IDbSet<SubtitleTask> Tasks { get; set; }

        public IDbSet<Movie> Movies { get; set; }

        public IDbSet<PromotionRequest> PromotionRequests { get; set; }


        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }       
}
