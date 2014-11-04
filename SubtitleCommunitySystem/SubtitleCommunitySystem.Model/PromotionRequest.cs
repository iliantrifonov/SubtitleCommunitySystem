namespace SubtitleCommunitySystem.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PromotionRequest
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public RequestType Type { get; set; }
    }
}
