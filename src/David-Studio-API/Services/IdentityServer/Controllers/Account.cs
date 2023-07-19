using Duende.IdentityServer.Services;
using IdentityServer.Dtos;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Account : ControllerBase
    {
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IServerUrls _serverUrls;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManger;

        public Account(
            IIdentityServerInteractionService interactionService,
            IServerUrls serverUrls,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManger)
        {
            _interactionService = interactionService;
            _serverUrls = serverUrls;
            _userManager = userManager;
            _signInManger = signInManger;
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            string? returnUrl = userLoginDto.ReturnUrl is not null
                ? Uri.UnescapeDataString(userLoginDto.ReturnUrl) : null;

            if (returnUrl is null ||
                await _interactionService.GetAuthorizationContextAsync(returnUrl) is null)
            {
                returnUrl = _serverUrls.BaseUrl;
            }

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

                return Ok(new { returnUrl });
            }
            else
            {
                var result = await _signInManger.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, true);

                return !result.Succeeded
                    ? Unauthorized("Wrong username or password")
                    : Ok(new { returnUrl });
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> Logout([FromQuery] string logoutId)
        {
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            if (logoutRequest is null ||
                (logoutRequest.ShowSignoutPrompt && User.Identity?.IsAuthenticated == true))
                return Ok(new { prompt = User.Identity?.IsAuthenticated ?? false });

            await _signInManger.SignOutAsync();

            return Ok(new
            {
                postLogoutRedirectUri = logoutRequest.PostLogoutRedirectUri
            });
        }
    }
}
