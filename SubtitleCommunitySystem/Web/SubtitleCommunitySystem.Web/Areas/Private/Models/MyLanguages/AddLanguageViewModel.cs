namespace SubtitleCommunitySystem.Web.Areas.Private.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class AddLanguageViewModel
    {
        [Display(Name = "Choose Language")]
        public string LanguageId { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }
    }
}