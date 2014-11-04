namespace SubtitleCommunitySystem.Model
{
    using System.Collections.Generic;

    public class Channel
    {
        public Channel()
        {
            this.Messages = new HashSet<Message>();
        }

        public int Id { get; set; }

        public virtual Team Team { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
