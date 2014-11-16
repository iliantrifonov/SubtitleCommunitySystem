namespace SubtitleCommunitySystem.Model
{
    using System.ComponentModel.DataAnnotations;

    public class DbFile
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        [Required]
        [MaxLength(50)]
        public string FileName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ContentType { get; set; }
    }
}
