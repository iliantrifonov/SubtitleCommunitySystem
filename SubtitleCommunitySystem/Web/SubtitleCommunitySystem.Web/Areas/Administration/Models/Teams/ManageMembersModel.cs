namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System.Collections.Generic;

    public class ManageMembersModel
    {
        public ManageMembersModel()
        {
            this.Translators = new HashSet<UserOutputModel>();
            this.ImageManagers = new HashSet<UserOutputModel>();
            this.Syncs = new HashSet<UserOutputModel>();
            this.Revisioners = new HashSet<UserOutputModel>();
            this.TeamLeaders = new HashSet<UserOutputModel>();
        }
        public int Id { get; set; }

        public IEnumerable<UserOutputModel> Translators { get; set; }

        public IEnumerable<UserOutputModel> ImageManagers { get; set; }

        public IEnumerable<UserOutputModel> Syncs { get; set; }

        public IEnumerable<UserOutputModel> Revisioners { get; set; }

        public IEnumerable<UserOutputModel> TeamLeaders { get; set; }
        
    }
}