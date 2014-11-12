namespace SubtitleCommunitySystem.Data
{
    using System;
    using System.Data.Entity;

    using SubtitleCommunitySystem.Model;
    using System.Data.Entity.Infrastructure;

    public interface IApplicationDbContext 
    {
        IDbSet<Channel> Channels { get; set; }

        IDbSet<DbFile> Files { get; set; }

        IDbSet<Language> Languages { get; set; }

        IDbSet<Message> Messages { get; set; }

        IDbSet<Movie> Movies { get; set; }

        IDbSet<PromotionRequest> PromotionRequests { get; set; }

        IDbSet<Subtitle> Subtitles { get; set; }

        IDbSet<SubtitleTask> Tasks { get; set; }

        IDbSet<TeamRole> TeamRoles { get; set; }

        IDbSet<Team> Teams { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
