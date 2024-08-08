namespace InforceTask.DTOs
{
    public class UrlListItemDTO
    {
        public int UrlId { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public string ShortenedUrl { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
    }
}
