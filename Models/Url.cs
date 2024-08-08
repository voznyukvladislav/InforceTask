namespace InforceTask.Models
{
    public class Url
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public string ShortenedUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = new();
        public int UserId { get; set; }
    }
}
