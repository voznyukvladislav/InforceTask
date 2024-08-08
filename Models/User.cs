namespace InforceTask.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public Role Role { get; set; } = new();
        public int RoleId { get; set; }

        public List<Url> Urls { get; set; } = new();
        public List<Description> Descriptions { get; set; } = new();
    }
}
