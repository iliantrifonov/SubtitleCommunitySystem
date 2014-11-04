namespace SubtitleCommunitySystem.Data
{
    using System;
    using System.Linq;

    using SubtitleCommunitySystem.Data.Repositories;
    using SubtitleCommunitySystem.Model;

    public interface IApplicationData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Language> Languages { get; }

        IRepository<Subtitle> Subtitles { get; }

        IRepository<Channel> Channels { get; }


        IRepository<Message> Messages { get; }

        IRepository<Team> Teams { get; }

        int SaveChanges();
    }
}
