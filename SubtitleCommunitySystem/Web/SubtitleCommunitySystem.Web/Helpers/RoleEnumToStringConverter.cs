namespace SubtitleCommunitySystem.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Constants;

    public class RoleEnumToStringConverter
    {
        public static string GetRole(RequestType type)
        {
            switch (type)
            {
                case RequestType.Translator:
                    return RoleConstants.Translator;
                case RequestType.Sync:
                    return RoleConstants.Sync;
                case RequestType.ImageManager:
                    return RoleConstants.ImageManager;
                case RequestType.Revisioner:
                    return RoleConstants.Revisioner;
                case RequestType.Writer:
                    return RoleConstants.Writer;
                case RequestType.TeamLeader:
                    return RoleConstants.TeamLeader;
                case RequestType.Moderator:
                    return RoleConstants.Moderator;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}