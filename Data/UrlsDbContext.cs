using InforceTask.Models;
using Microsoft.EntityFrameworkCore;

namespace InforceTask.Data
{
    public class UrlsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Url> Urls { get; set; }
        public DbSet<Description> Descriptions { get; set; }

        public UrlsDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
