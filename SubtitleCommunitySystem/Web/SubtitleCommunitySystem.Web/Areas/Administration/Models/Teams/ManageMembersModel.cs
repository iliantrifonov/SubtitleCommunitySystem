namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System.Collections.Generic;

    public class ManageMembersModel
    {
        public int Id { get; set; }

        public IEnumerable<UserOutputModel> Translators { get; set; }
    }
}