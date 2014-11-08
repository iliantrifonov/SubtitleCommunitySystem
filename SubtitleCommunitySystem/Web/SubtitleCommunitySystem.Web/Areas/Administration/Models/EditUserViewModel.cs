namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System.Collections.Generic;

    public class EditUserViewModel
    {
        public UserInputModel User { get; set; }

        public IEnumerable<TeamRoleModel> TeamRoles { get; set; }
    }
}