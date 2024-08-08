using InforceTask.Data;
using InforceTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InforceTask.Controllers
{
    [Route("api/seed")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private UrlsDbContext DbContext { get; set; }
        public SeedController(UrlsDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        [HttpPost]
        [Route("roles")]
        public IActionResult SeedRoles()
        {
            if (this.DbContext.Roles.ToList().Count == 0)
            {
                List<Role> roles = new()
                {
                    new Role() { Name = Constants.Admin },
                    new Role() { Name = Constants.User }
                };
                this.DbContext.Roles.AddRange(roles);
                this.DbContext.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("admin")]
        public IActionResult SeedAdmin()
        {
            string adminLogin = "Vladosek";
            if (this.DbContext.Users.FirstOrDefault(u => u.Login == adminLogin) is null)
            {
                Role role = this.DbContext.Roles.First(r => r.Name == Constants.Admin);

                string password = "gfhjkmd";
                User user = new User()
                {
                    Login = adminLogin,
                    Password = Hasher.Hash(password),
                    Role = role,
                    RoleId = role.Id
                };

                this.DbContext.Users.Add(user);
                this.DbContext.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
    }
}
