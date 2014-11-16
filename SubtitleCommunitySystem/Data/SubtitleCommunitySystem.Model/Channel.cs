namespace SubtitleCommunitySystem.Model
{
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    public class Channel
    {
        public Channel()
        {
            this.Messages = new HashSet<Message>();
            this.Teams = new HashSet<Team>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
