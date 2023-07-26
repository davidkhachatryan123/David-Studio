using Duende.IdentityServer.Events;
using Duende.IdentityServer.Services;
using IdentityServer.Dtos;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static IdentityModel.OidcConstants;

namespace IdentityServer.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    public class Account : ControllerBase
    {
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IServerUrls _serverUrls;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly IEventService _events;

        public Account(
            IIdentityServerInteractionService interactionService,
            IServerUrls serverUrls,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManger,
            IEventService events)
        {
            _interactionService = interactionService;
            _serverUrls = serverUrls;
            _userManager = userManager;
            _signInManger = signInManger;
            _events = events;
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("/api/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            string? returnUrl = userLoginDto.ReturnUrl is not null
                ? Uri.UnescapeDataString(userLoginDto.ReturnUrl) : null;

            ApplicationUser? user = await _userManager.FindByNameAsync(userLoginDto.Username);

            if (user is null)
                return BadRequest();
            else if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                // TODO: Send confirmation email

                return Unauthorized("Email doesn't confirmed! Confirmation email sended to your Email");
            }

            if (user.TwoFactorEnabled)
            {
                if (!await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
                    return BadRequest();

                string code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);

                // TODO: Send MFA code

                return Ok(new { mfa = true });
            }
            else
            {
                var result = await _signInManger.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, true);

                if (!result.Succeeded)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(userLoginDto.Username, "Invalid credentials"));
                    return Unauthorized("Wrong username or password");
                }

                await _events.RaiseAsync(new UserLoginSuccessEvent(userLoginDto.Username, user.Id, userLoginDto.Username));
                return Ok(new { returnUrl });
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("/api/logout")]
        public async Task<IActionResult> Logout([FromQuery] string logoutId)
        {
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            if (logoutRequest is null || logoutRequest.ShowSignoutPrompt)
                return Ok(new { prompt = logoutRequest?.ShowSignoutPrompt ?? false });

            await _signInManger.SignOutAsync();

            return Ok(new
            {
                postLogoutRedirectUri = logoutRequest.PostLogoutRedirectUri
            });
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("/api/logout")]
        public async Task<IActionResult> PostLogout([FromQuery] string logoutId)
        {
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            await _signInManger.SignOutAsync();

            return Ok(new
            {
                postLogoutRedirectUri = logoutRequest.PostLogoutRedirectUri
            });
        }
    }
}
