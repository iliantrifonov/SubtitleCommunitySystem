using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleCommunitySystem.Model
{
    public class Channel
    {
        public Channel()
        {
            this.Messages = new HashSet<Message>();
        }

        public int Id { get; set; }

        public Team Team { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
