namespace SubtitleCommunitySystem.Model
{
    public class DbFile
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }
    }
}
