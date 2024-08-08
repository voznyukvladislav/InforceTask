using InforceTask.Data;
using InforceTask.Models;
using Microsoft.IdentityModel.Tokens;

namespace InforceTask.Services
{
    public class IndexService
    {
        public UrlsDbContext DbContext { get; set; }
        public AuthService AuthService { get; set; }

        public IndexService(UrlsDbContext dbContext, AuthService authService) {
            this.DbContext = dbContext;
            this.AuthService = authService;
        }

        public Description GetDescription()
        {
            Description? description = this.DbContext.Descriptions.FirstOrDefault();
            return description is null ? new Description() : description;
        }

        public void ChangeDescription(string descriptionText, User user)
        {
            Description description = new Description()
            {
                User = user,
                UserId = user.Id,
                Value = descriptionText
            };

            this.DbContext.Descriptions.RemoveRange(this.DbContext.Descriptions.ToList());
            this.DbContext.SaveChanges();

            this.DbContext.Descriptions.Add(description);
            this.DbContext.SaveChanges();

            this.DbContext.ChangeTracker.Clear();
        }

        public string GetOriginalUrl(string shortenedUrl)
        {
            if (shortenedUrl.IsNullOrEmpty()) throw new Exception();

            string urlStr = $"{Constants.Api}/{shortenedUrl}";

            Url? url = this.DbContext.Urls.FirstOrDefault(u => u.ShortenedUrl == urlStr);
            if (url is null) throw new Exception();

            return url.OriginalUrl;
        }
    }
}
