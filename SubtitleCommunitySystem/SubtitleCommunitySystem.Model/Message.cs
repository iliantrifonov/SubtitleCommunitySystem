
namespace SubtitleCommunitySystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Message
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateSent { get; set; }

        public bool IsRead { get; set; }
    }
}
