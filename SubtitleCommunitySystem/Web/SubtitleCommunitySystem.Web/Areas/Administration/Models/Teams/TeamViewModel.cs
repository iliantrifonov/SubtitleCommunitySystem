﻿namespace SubtitleCommunitySystem.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using SubtitleCommunitySystem.Model;

    public class TeamViewModel
    {
        public static Expression<Func<Team, TeamViewModel>> FromTeam
        {
            get
            {
                return t => new TeamViewModel()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Language = new LanguageOutputModel()
                        {
                            Id = t.Language.Id,
                            Name = t.Language.Name
                        },
                        Members = t.Members.AsQueryable().Select(m => new UserOutputModel()
                        {
                            Id = m.Id,
                            UserName = m.UserName
                        })
                    };
            }
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public LanguageOutputModel Language { get; set; }

        public IEnumerable<UserOutputModel> Members { get; set; }
    }
}
