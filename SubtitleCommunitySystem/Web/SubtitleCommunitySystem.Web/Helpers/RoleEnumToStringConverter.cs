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
        public static string FromRequestType(RequestType type)
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

        internal static string FromSubtitleTaskType(SubtitleTaskType type)
        {
            switch (type)
            {
                case SubtitleTaskType.Translate:
                    return RoleConstants.Translator;
                case SubtitleTaskType.Sync:
                    return RoleConstants.Sync;
                case SubtitleTaskType.Revision:
                    return RoleConstants.Revisioner;
                case SubtitleTaskType.Image:
                    return RoleConstants.ImageManager;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}