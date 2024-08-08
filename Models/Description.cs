namespace InforceTask.Models
{
    public class Description
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public User User { get; set; } = new();
        public int UserId { get; set; }
    }
}
