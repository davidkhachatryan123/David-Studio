using System.Collections;
using AutoMapper;
using Google.Protobuf.Collections;
using Grpc.Core;
using IdentityServer.Enums;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Services.Common.Extensions;
using Services.Common.Models;

namespace IdentityServer.Grpc.Services
{
    public class ManageUsersService : ManageUsers.ManageUsersBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<TokenResponse> GetEmailConfirmationToken(User request, ServerCallContext context)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(request.Id);

            return user is null
                ? new TokenResponse()
                : new TokenResponse
                {
                    Email = user.Email,
                    Token = await _userManager.GenerateEmailConfirmationTokenAsync(user)
                };
        }
    }
}
