using InforceTask.Data;
using InforceTask.DTOs;
using InforceTask.Models;
using InforceTask.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InforceTask.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService AuthService { get;  set; }
        public AuthController(AuthService authService)
        {
            this.AuthService = authService;
        }

        // Main endpoints
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(AuthDTO auth)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = this.AuthService.Login(auth);
                await this.HttpContext.SignInAsync(claimsPrincipal);

                MessageDTO message = MessageDTO.CreateSuccessful(Constants.TITLE_LOGIN, Constants.LOGIN_SUCC);
                return Ok(message);
            }
            catch
            {
                MessageDTO message = MessageDTO.CreateFailed(Constants.TITLE_LOGIN, Constants.LOGIN_FAIL);
                return BadRequest(message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                await this.HttpContext.SignOutAsync();

                MessageDTO message = MessageDTO.CreateSuccessful(Constants.TITLE_LOGOUT, Constants.LOGOUT_SUCC);
                return Ok(message);
            }
            catch
            {
                MessageDTO message = MessageDTO.CreateFailed(Constants.TITLE_LOGOUT, Constants.LOGOUT_FAIL);
                return BadRequest(message);
            }
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegistrationAsync(AuthDTO auth)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = this.AuthService.Registration(auth);
                await this.HttpContext.SignInAsync(claimsPrincipal);

                MessageDTO message = MessageDTO.CreateSuccessful(Constants.TITLE_REGISTRATION, Constants.REGISTRATION_SUCC);
                return Ok(message);
            }
            catch
            {
                MessageDTO message = MessageDTO.CreateFailed(Constants.TITLE_REGISTRATION, Constants.REGISTRATION_FAIL);
                return BadRequest(message);
            }
        }

        // Additional endpoints
        [HttpGet]
        [Route("isAuthenticated")]
        public IActionResult IsAuthenticated()
        {
            bool? isAuthenticated = this.HttpContext.User.Identity?.IsAuthenticated;
            return Ok(isAuthenticated);
        }

        [HttpGet]
        [Route("isAdmin")]
        public IActionResult IsAdmin()
        {
            bool isAdmin = this.AuthService.IsAdmin(this.HttpContext);
            return Ok(isAdmin);
        }

        [Authorize]
        [HttpGet]
        [Route("userId")]
        public IActionResult UserId()
        {
            User? user = this.AuthService.GetUser(this.HttpContext);
            return Ok(user.Id);
        }
    }
}
