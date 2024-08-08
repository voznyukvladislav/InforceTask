﻿using InforceTask.Data;
using InforceTask.DTOs;
using InforceTask.Models;
using InforceTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InforceTask.Controllers
{
    [Route("")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        public UrlsDbContext DbContext { get; set; }
        public AuthService AuthService { get; set; }
        public IndexService IndexService { get; set; }
        public IndexController(UrlsDbContext dbContext, AuthService authService, IndexService indexService)
        {
            this.DbContext = dbContext;
            this.AuthService = authService;
            this.IndexService = indexService;
        }

        [HttpGet]
        [Route("{shortenedUrl}")]
        public IActionResult RedirectTo(string shortenedUrl)
        {
            try
            {
                string originalUrl = this.IndexService.GetOriginalUrl(shortenedUrl);
                return Redirect(originalUrl);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("about")]
        public IActionResult About()
        {
            Description description = this.IndexService.GetDescription();
            return Ok(description);
        }

        [Authorize(Roles = Constants.Admin)]
        [HttpPost]
        [Route("about")]
        public IActionResult About(string descriptionText) 
        {
            try
            {
                User user = this.AuthService.GetUser(this.HttpContext);
                this.IndexService.ChangeDescription(descriptionText, user);

                MessageDTO message = MessageDTO.CreateSuccessful(Constants.TITLE_DESCRIPTION_CHANGE, Constants.DESC_CHANGE_SUCC); 
                return Ok(message);
            }
            catch
            {
                MessageDTO message = MessageDTO.CreateFailed(Constants.TITLE_DESCRIPTION_CHANGE, Constants.DESC_CHANGE_FAIL);
                return BadRequest(message);
            }
        }
    }
}
