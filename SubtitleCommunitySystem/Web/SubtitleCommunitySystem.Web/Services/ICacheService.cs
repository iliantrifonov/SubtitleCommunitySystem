namespace SubtitleCommunitySystem.Web.Services
{
    using System;
    using System.Collections;

    public interface ICacheService
    {
        IEnumerable GetDropDownForUsers(string roleName, int? teamId);

        IEnumerable GetTop200SearchResults(string searchString);
    }
}
