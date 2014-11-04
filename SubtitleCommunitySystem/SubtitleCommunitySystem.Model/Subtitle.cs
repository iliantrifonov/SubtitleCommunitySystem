namespace SubtitleCommunitySystem.Model
{
    public class Subtitle
    {
        public int Id { get; set; }

        public bool IsFinished { get; set; }

        public Language Language { get; set; }

        public Team Team { get; set; }
    }
}
