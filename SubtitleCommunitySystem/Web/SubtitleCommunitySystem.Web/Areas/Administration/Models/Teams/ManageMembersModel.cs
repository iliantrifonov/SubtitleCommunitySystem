namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System.Collections.Generic;

    public class ManageMembersModel
    {
        public int Id { get; set; }

        public IEnumerable<UserOutputModel> Translators { get; set; }

        public IEnumerable<UserOutputModel> ImageManagers { get; set; }

        public IEnumerable<UserOutputModel> Syncs { get; set; }

        public IEnumerable<UserOutputModel> Revisioners { get; set; }

        public IEnumerable<UserOutputModel> TeamLeaders { get; set; }
        
    }
}