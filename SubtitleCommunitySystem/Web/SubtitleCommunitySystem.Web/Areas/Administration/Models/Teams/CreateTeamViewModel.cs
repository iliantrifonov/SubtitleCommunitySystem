namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class CreateTeamViewModel
    {
        public TeamInputModel Team { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }
    }
}