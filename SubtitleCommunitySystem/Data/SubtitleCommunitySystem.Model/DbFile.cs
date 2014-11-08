using System.ComponentModel.DataAnnotations;
namespace SubtitleCommunitySystem.Model
{
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
