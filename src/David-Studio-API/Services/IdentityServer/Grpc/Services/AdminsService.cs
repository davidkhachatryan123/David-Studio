using System.Collections;
using AutoMapper;
using Google.Protobuf.Collections;
using Grpc.Core;
using IdentityServer.Enums;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Services.Common.Extensions;
using Services.Common.Models;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace IdentityServer.Grpc.Services
{
    public class AdminsService : Admins.AdminsBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IMapper _mapper;

        public AdminsService(
            UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IMapper mapper)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public override async Task<AdminsResponse> GetAll(PageData request, ServerCallContext context)
        {
            PageData<ApplicationUser> users =
                await _userManager.Users.ToPagedAsync(_mapper.Map<PageOptions>(request));

            var response = new AdminsResponse
            {
                Status = AccountStatus.Successfull,
                TotalCount = users.TotalCount
            };

            response.Users.AddRange(_mapper.Map<IEnumerable<AdminReadData>>(users.Entities));

            return response;
        }

        public override async Task<AdminResponse> GetById(AdminIdData request, ServerCallContext context)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(request.Id);

            return user is not null
                ? new AdminResponse
                {
                    Status = AccountStatus.Successfull,
                    User = _mapper.Map<AdminReadData>(user)
                }
                : new AdminResponse { Status = AccountStatus.ErrorOccurred };
        }

        public override async Task<AdminResponse> Create(AdminCreateData request, ServerCallContext context)
        {
            ApplicationUser newUser = _mapper.Map<ApplicationUser>(request);
            newUser.TwoFactorEnabled = true;

            var result = await _userManager.CreateAsync(newUser, request.Password);
            result = await _userManager.AddToRoleAsync(newUser, nameof(UserRoles.Admin));

            return result.Succeeded
                ? new AdminResponse
                {
                    Status = AccountStatus.Successfull,
                    User = _mapper.Map<AdminReadData>(newUser)
                }
                : new AdminResponse { Status = AccountStatus.ErrorOccurred };
        }

        public override async Task<AdminResponse> Update(AdminUpdateData request, ServerCallContext context)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(request.Id);

            if (user is null)
                return new AdminResponse { Status = AccountStatus.ErrorOccurred };

            user.UserName = request.User.Username;
            user.Email = request.User.Email;
            user.EmailConfirmed = false;
            user.PasswordHash = _passwordHasher.HashPassword(user, request.User.Password);
            user.PhoneNumber = request.User.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded
                ? new AdminResponse
                {
                    Status = AccountStatus.Successfull,
                    User = _mapper.Map<AdminReadData>(user)
                }
                : new AdminResponse { Status = AccountStatus.ErrorOccurred };
        }

        public override async Task<AdminResponse> Delete(AdminIdData request, ServerCallContext context)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(request.Id);

            if (user is null)
                return new AdminResponse { Status = AccountStatus.ErrorOccurred };

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded
                ? new AdminResponse
                {
                    Status = AccountStatus.Successfull,
                    User = _mapper.Map<AdminReadData>(user)
                }
                : new AdminResponse { Status = AccountStatus.ErrorOccurred };
        }
    }
}
