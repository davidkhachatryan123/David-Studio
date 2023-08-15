using System;
using Grpc.Net.Client;
using Google.Protobuf;
using Microsoft.Extensions.Configuration;
using Users.Dtos;
using Services.Common.Models;
using AutoMapper;
using Azure;

namespace Users.Grpc.Clients
{
    public class UsersDataClient : IUsersDataClient, IDisposable
    {
        private readonly IMapper _mapper;

        private readonly string? uri;
        private readonly GrpcChannel _channel;
        private readonly Users.UsersClient _client;

        public UsersDataClient(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;

            uri = configuration.GetValue<string>("Services:IdentityServerGrpcUri");
            if (uri is null) throw new Exception("Storage service uri is null");

            _channel = GrpcChannel.ForAddress(uri!);
            _client = new Users.UsersClient(_channel);
        }

        public async Task<PageData<UserReadDto>> GetAllAsync(PageOptions options)
        {
            var reply = await _client.GetAllAsync(_mapper.Map<PageData>(options));

            if (reply.Status == AccountStatus.ErrorOccurred)
                throw new Exception("Error occurred while trying to get all users");

            return new PageData<UserReadDto>
            {
                Entities = _mapper.Map<IEnumerable<UserReadDto>>(reply.Users),
                TotalCount = reply.Users.Count
            };
        }

        public async Task<UserReadDto?> GetByIdAsync(string id)
        {
            var reply = await _client.GetByIdAsync(new UserIdData
            {
                Id = id
            });

            return reply.Status == AccountStatus.Successfull
                ? _mapper.Map<UserReadDto>(reply.User)
                : null;
        }

        public async Task<UserReadDto?> CreateAsync(UserCreateDto user)
        {
            var reply = await _client.CreateAsync(_mapper.Map<UserCreateData>(user));

            return reply.Status == AccountStatus.Successfull
                ? _mapper.Map<UserReadDto>(reply.User)
                : null;
        }

        public async Task<UserReadDto?> UpdateAsync(string id, UserCreateDto user)
        {
            var reply = await _client.UpdateAsync(new UserUpdateData
            {
                Id = id,
                User = _mapper.Map<UserCreateData>(user)
            });

            return reply.Status == AccountStatus.Successfull
                ? _mapper.Map<UserReadDto>(reply.User)
                : null;
        }

        public async Task<UserReadDto?> DeleteAsync(string id)
        {
            var reply = await _client.DeleteAsync(new UserIdData
            {
                Id = id
            });

            return reply.Status == AccountStatus.Successfull
                ? _mapper.Map<UserReadDto>(reply.User)
                : null;
        }

        public void Dispose()
        {
            _channel.Dispose();
        }
    }
}
