using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using EventBus.Abstractions;
using EventBus.Events;
using IdentityServer.Dtos;
using IdentityServer.IntegrationEvents.Events;
using IdentityServer.Models;
using IdentityServer.RepositoryManager.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using static Duende.IdentityServer.Models.IdentityResources;

namespace IdentityServer.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Account : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IEventService _events;
        private readonly IServerUrls _serverUrls;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Account> _logger;

        private readonly IRepositoryManager _repositoryManager;
        private readonly IEventBus _eventBus;

        public Account(
            SignInManager<ApplicationUser> signInManger,
            UserManager<ApplicationUser> userManager,
            IIdentityServerInteractionService interactionService,
            IEventService events,
            IServerUrls serverUrls,
            IConfiguration configuration,
            ILogger<Account> logger,
            IRepositoryManager repositoryManager,
            IEventBus eventBus)
        {
            _interactionService = interactionService;
            _events = events;
            _signInManager = signInManger;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _serverUrls = serverUrls;

            _repositoryManager = repositoryManager;
            _eventBus = eventBus;
        }

        [AllowAnonymous]
        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route(nameof(Login))]
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
                await _repositoryManager.ManageUsers.SendConfirmationEmailAsync(new ConfirmEmailRequestDto()
                {
                    UserId = user.Id,
                    ReturnUrl = returnUrl
                });

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
                await _repositoryManager.ManageUsers.SendMfaCodeAsync(user.Email!);
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
        [Route(nameof(LoginMfa))]
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
        [Route(nameof(ConfirmEmail))]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto confirmEmailDto)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(confirmEmailDto.UserId);
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
        [Route(nameof(Logout))]
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
        [Route(nameof(Logout))]
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
