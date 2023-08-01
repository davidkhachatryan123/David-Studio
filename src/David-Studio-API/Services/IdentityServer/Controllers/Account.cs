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
using Microsoft.Extensions.Configuration;

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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEventService _events;
        private readonly IEventBus _eventBus;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Account> _logger;

        public Account(
            IIdentityServerInteractionService interactionService,
            IServerUrls serverUrls,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManger,
            IEventService events,
            IEventBus eventBus,
            IConfiguration configuration,
            ILogger<Account> logger)
        {
            _interactionService = interactionService;
            _serverUrls = serverUrls;
            _userManager = userManager;
            _signInManager = signInManger;
            _events = events;
            _eventBus = eventBus;
            _configuration = configuration;
            _logger = logger;
        }

        [AllowAnonymous]
        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("/api/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            string? returnUrl = userLoginDto.ReturnUrl is not null
                ? Uri.UnescapeDataString(userLoginDto.ReturnUrl)
                : _configuration.GetValue<string>("SpaClient");

            ApplicationUser? user = await _userManager.FindByNameAsync(userLoginDto.Username);

            if (user is null)
                return BadRequest();
            else if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                await SendConfirmationEmail(user.Email!, returnUrl);

                return Unauthorized("Email doesn't confirmed! Confirmation email sended to your Email");
            }

            var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, true);

            if (result.Succeeded)
            {
                await _events.RaiseAsync(new UserLoginSuccessEvent(userLoginDto.Username, user.Id, userLoginDto.Username));
                return Ok(new { returnUrl });
            }
            else if (result.RequiresTwoFactor)
            {
                await SendMfaCode(user.Email!);
                return Ok(new { mfa = true, returnUrl, userLoginDto.RememberMe });
            }
            else
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(userLoginDto.Username, "Invalid credentials"));
                return Unauthorized("Wrong username or password");
            }
        }

        [AllowAnonymous]
        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("/api/login/mfa")]
        public async Task<IActionResult> LoginMfa([FromBody] UserTwoFactorLoginDto userTwoFactorLoginDto)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user is null)
                return BadRequest("User not found");

            var result = await _signInManager.TwoFactorSignInAsync(
                TokenOptions.DefaultEmailProvider,
                userTwoFactorLoginDto.Code,
                userTwoFactorLoginDto.RememberMe,
                false);

            string? returnUrl = userTwoFactorLoginDto.ReturnUrl is not null
                ? Uri.UnescapeDataString(userTwoFactorLoginDto.ReturnUrl)
                : _configuration.GetValue<string>("SpaClient");

            if (!result.Succeeded)
            {
                await _events.RaiseAsync(new UserLoginFailureEvent(user.UserName, "Invalid credentials"));
                return Unauthorized("Two factor code is not valid");
            }

            await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));
            return Ok(new { returnUrl });
        }

        [AllowAnonymous]
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("/api/sendMfaCode")]
        public async Task<IActionResult> SendMfaCode([FromQuery] string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            if (user is null) return NotFound("User not found!");

            string code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);

            _logger.LogInformation("Publishing message to event bus for send email to: {EmailAddress}", user.Email);

            IntegrationEvent @event = new SendTwoFactorCodeEmailIntegrationEvent(user.Email!, code);
            _eventBus.Publish(@event);

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("/api/sendConfirmationEmail")]
        public async Task<IActionResult> SendConfirmationEmail([FromQuery] string email, [FromQuery] string? returnUrl = null)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            if (user is null) return NotFound("User not found!");

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string? confirmUrl = Url.Action(nameof(ConfirmEmail), nameof(Account),
                new ConfirmEmailDto { Email = email, Token = token, ReturnUrl = returnUrl },
                Request.Scheme);
            if (confirmUrl is null) return BadRequest();

            _logger.LogInformation("Publishing message to event bus for user email confirmation: {EmailAddress}", user.Email);

            IntegrationEvent @event = new SendConfirmationEmailIntegrationEvent(user.Email!, confirmUrl);
            _eventBus.Publish(@event);

            return Ok();
        }

        [AllowAnonymous]
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("/api/confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto confirmEmailDto)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            if (user is null) return NotFound("User not found!");

            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
            if (!result.Succeeded) return BadRequest();

            return confirmEmailDto.ReturnUrl is null
                ? Ok()
                : Redirect(confirmEmailDto.ReturnUrl);
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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
