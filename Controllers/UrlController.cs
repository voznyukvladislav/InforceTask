using InforceTask.Data;
using InforceTask.DTOs;
using InforceTask.Models;
using InforceTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InforceTask.Controllers
{
    [Route("api/url")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        public UrlService UrlService { get; set; }
        public AuthService AuthService { get; set; }
        public UrlsDbContext DbContext { get; set; }
        public UrlController(UrlService urlService, AuthService authService, UrlsDbContext dbContext)
        {
            this.UrlService = urlService;
            this.AuthService = authService;
            this.DbContext = dbContext;
        }

        [Authorize]
        [HttpPost]
        [Route("addUrl")]
        public IActionResult AddUrl(string originalUrl, string shortenedUrl)
        {
            try
            {
                if (!this.UrlService.IsValidUrl(originalUrl)) throw new ArgumentException();
                if (this.UrlService.HasDuplicate(originalUrl)) throw new ArgumentException();

                User user = this.AuthService.GetUser(this.HttpContext);
                Url url = this.UrlService.GetUrl(originalUrl, shortenedUrl, user);

                this.DbContext.Urls.Add(url);
                this.DbContext.SaveChanges();

                MessageDTO message = MessageDTO.CreateSuccessful(Constants.TITLE_ADD_URL, Constants.ADD_URL_SUCC);
                return Ok(message);
            }
            catch
            {
                MessageDTO message = MessageDTO.CreateFailed(Constants.TITLE_ADD_URL, Constants.ADD_URL_FAIL);
                return BadRequest(message);
            }
            
        }

        [Authorize]
        [HttpPost]
        [Route("checkUrl")]
        public IActionResult CheckUrl(string originalUrl, int hashLength)
        {            
            string shortenedUrl = this.UrlService.GetShortUrl(originalUrl, hashLength);

            return Ok(new { shortenedUrl });
        }

        [HttpGet]
        [Route("urls")]
        public IActionResult GetUrls()
        {
            List<UrlListItemDTO> urls = this.UrlService.GetUrls();

            return Ok(urls);
        }

        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUrl(int urlId, int userId)
        {
            try
            {
                this.UrlService.DeleteUrl(urlId, userId);

                MessageDTO message = MessageDTO.CreateSuccessful(Constants.TITLE_DEL_URL, Constants.DEL_URL_SUCC);
                return Ok(message);
            }
            catch
            {
                MessageDTO message = MessageDTO.CreateFailed(Constants.TITLE_DEL_URL, Constants.DEL_URL_FAIL);
                return BadRequest(message);
            }
        }
    }
}
