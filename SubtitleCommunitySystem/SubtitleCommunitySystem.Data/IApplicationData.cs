namespace SubtitleCommunitySystem.Data
{
    using System;
    using System.Linq;

    using SubtitleCommunitySystem.Data.Repositories;
    using SubtitleCommunitySystem.Model;

    public interface IApplicationData
    {
        IRepository<ApplicationUser> Users { get; }

        int SaveChanges();
    }
}
