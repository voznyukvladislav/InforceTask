using InforceTask.Data;
using InforceTask.DTOs;
using InforceTask.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace InforceTask.Services
{
    public class AuthService
    {
        private UrlsDbContext DbContext { get; set; }
        public AuthService(UrlsDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public ClaimsPrincipal Login(AuthDTO auth)
        {
            User? user = this.GetUser(auth);

            if (user is null) throw new ArgumentException();

            ClaimsPrincipal claimsPrincipal = this.GetClaimsPrincipal(user);

            return claimsPrincipal;
        }

        public ClaimsPrincipal Registration(AuthDTO auth)
        {
            if (!this.IsAvailableLogin(auth.Login)) throw new ArgumentException();

            User user = this.RegisterUser(auth);
            ClaimsPrincipal claimsPrincipal = this.GetClaimsPrincipal(user);

            return claimsPrincipal;
        }

        public ClaimsPrincipal GetClaimsPrincipal(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(ClaimTypes.Name, user.Login)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }

        public User RegisterUser(AuthDTO auth)
        {
            if (auth.Login.IsNullOrEmpty() || auth.Password.IsNullOrEmpty())
            {
                throw new ArgumentException();
            }

            string passwordHash = Hasher.Hash(auth.Password);

            Role role = this.DbContext.Roles.First(r => r.Name == Constants.User);
            User user = new User()
            {
                Login = auth.Login,
                Password = passwordHash,
                RoleId = role.Id,
                Role = role
            };

            this.DbContext.Users.Add(user);
            this.DbContext.SaveChanges();

            return user;
        }

        public User? GetUser(AuthDTO auth)
        {
            string passwordHash = Hasher.Hash(auth.Password);

            return this.DbContext.Users
                .Where(u => u.Login == auth.Login && u.Password == passwordHash)
                .Include(u => u.Role)
                .FirstOrDefault();
        }

        public User GetUser(HttpContext context)
        {
            int? userId = context.User.Identities
                .FirstOrDefault()?
                .Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => Convert.ToInt32(c.Value))
                .FirstOrDefault();

            User user = this.DbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Role)
                .First();

            return user;
        }

        public bool IsAdmin(HttpContext context)
        {
            int? userId = context.User.Identities
                .FirstOrDefault()?
                .Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => Convert.ToInt32(c.Value))
                .FirstOrDefault();

            if (userId is null || userId == 0) return false;

            User user = this.DbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Role)
                .First();

            if (user.Role.Name == Constants.Admin) return true;
            else return false;
        }

        public bool IsAvailableLogin(string login)
        {
            User? user = this.DbContext.Users.FirstOrDefault(u => u.Login == login);

            if (user is null) return true;
            else return false;
        }
    }
}
