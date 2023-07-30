using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using EventBus.Abstractions;
using EventBus.Events;
using IdentityServer.Dtos;
using IdentityServer.IntegrationEvents.Events;
using IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEventService _events;
        private readonly IEventBus _eventBus;
        private readonly ILogger<Account> _logger;

        public Account(
            IIdentityServerInteractionService interactionService,
            IServerUrls serverUrls,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManger,
            IEventService events,
            IEventBus eventBus,
            ILogger<Account> logger)
        {
            _interactionService = interactionService;
            _serverUrls = serverUrls;
            _userManager = userManager;
            _signInManager = signInManger;
            _events = events;
            _eventBus = eventBus;
            _logger = logger;
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

                _logger.LogInformation("Publishing message to event bus for send email to: {EmailAddress}", user.Email);
                IntegrationEvent @event = new SendEmailIntegrationEvent(user.Email!, "David Studio - 2FA Code", code);
                _eventBus.Publish(@event);

                return Ok(new { mfa = true });
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, true);

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

            if (logoutRequest is null || (logoutRequest.ShowSignoutPrompt && User.Identity?.IsAuthenticated == true))
                return Ok(new { prompt = User.Identity?.IsAuthenticated ?? false });

            await _signInManager.SignOutAsync();

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

            if (User.Identity?.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();

                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            return Ok(new
            {
                postLogoutRedirectUri = logoutRequest.PostLogoutRedirectUri
            });
        }
    }
}
