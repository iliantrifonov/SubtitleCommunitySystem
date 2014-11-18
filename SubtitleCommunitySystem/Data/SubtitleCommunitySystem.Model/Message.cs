﻿namespace SubtitleCommunitySystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Message
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public DateTime DateSent { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
