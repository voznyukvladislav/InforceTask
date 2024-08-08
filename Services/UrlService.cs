using InforceTask.Data;
using InforceTask.DTOs;
using InforceTask.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InforceTask.Services
{
    public class UrlService
    {
        public UrlsDbContext DbContext { get; set; }
        public UrlService(UrlsDbContext dbContext) 
        {
            this.DbContext = dbContext;
        }

        public string GetShortUrl(string originalUrl, int hashLenght)
        {
            if (originalUrl.IsNullOrEmpty()) return string.Empty;

            string hash = Hasher.Hash(originalUrl);
            Random random = new Random();

            StringBuilder sb = new StringBuilder();
            sb.Append($"{Constants.Api}/");
            for (int i = 0; i < hashLenght; i++)
            {
                sb.Append(hash[random.Next(0, hash.Length)]);
            }
            string shortenedUrl = sb.ToString();
            sb.Clear();

            if (this.DbContext.Urls.FirstOrDefault(u => u.ShortenedUrl == shortenedUrl) is not null)
            {
                shortenedUrl = this.GetShortUrl(originalUrl, hashLenght);
            }

            return shortenedUrl;
        }

        public Url GetUrl(string originalUrl, string shortenedUrl, User user)
        {
            Url url = new Url()
            {
                OriginalUrl = originalUrl,
                ShortenedUrl = shortenedUrl,
                User = user,
                UserId = user.Id,
                CreatedAt = DateTime.Now
            };

            return url;
        } 

        public List<UrlListItemDTO> GetUrls()
        {
            List<UrlListItemDTO> urlsDto = new();
            List<Url> urls = this.DbContext.Urls.ToList();

            urlsDto = urls.Select(u => new UrlListItemDTO()
            {
                UrlId = u.Id,
                UserId = u.UserId,
                OriginalUrl = u.OriginalUrl,
                ShortenedUrl = u.ShortenedUrl,
                CreatedAt = u.CreatedAt.ToString("dd.MM.yyyy hh:mm")
            }).ToList();

            return urlsDto;
        }

        public void DeleteUrl(int urlId, int userId)
        {
            User user = this.DbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Role)
                .First();

            Url url = this.DbContext.Urls.First(u => u.Id == urlId);

            if (user.Id == url.UserId || user.Role.Name == Constants.Admin)
            {
                this.DbContext.Urls.Remove(url);
                this.DbContext.SaveChanges();
            }
        }

        public bool IsValidUrl(string url)
        {
            bool success = Uri.TryCreate(url, UriKind.Absolute, out Uri? uri);
            return success;
        }

        public bool HasDuplicate(string originalUrl)
        {
            Url? url = this.DbContext.Urls.FirstOrDefault(u => u.OriginalUrl == originalUrl);
            if (url is null) return false;
            else return true;
        }
    }
}
