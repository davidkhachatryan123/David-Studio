﻿using Grpc.Net.Client;
using Users.Dtos;
using Services.Common.Models;
using AutoMapper;

namespace Users.Grpc.Clients
{
    public class AdminsDataClient : IAdminsDataClient, IDisposable
    {
        private readonly IMapper _mapper;

        private readonly string? uri;
        private readonly GrpcChannel _channel;
        private readonly Admins.AdminsClient _client;

        public AdminsDataClient(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;

            uri = configuration.GetValue<string>("Services:IdentityServerGrpcUri");
            if (uri is null) throw new Exception("Services:IdentityServerGrpcUri variable is null");

            _channel = GrpcChannel.ForAddress(uri!);
            _client = new Admins.AdminsClient(_channel);
        }

        public async Task<PageData<AdminReadDto>> GetAllAsync(PageOptions options)
        {
            var reply = await _client.GetAllAsync(_mapper.Map<PageData>(options));

            if (reply.Status == AccountStatus.ErrorOccurred)
                throw new Exception("Error occurred while trying to get all users");

            return new PageData<AdminReadDto>
            {
                Entities = _mapper.Map<IEnumerable<AdminReadDto>>(reply.Users),
                TotalCount = reply.TotalCount
            };
        }

        public async Task<AdminReadDto?> GetByIdAsync(string id)
        {
            var reply = await _client.GetByIdAsync(new AdminIdData
            {
                Id = id
            });

            return reply.Status == AccountStatus.Successfull
                ? _mapper.Map<AdminReadDto>(reply.User)
                : null;
        }

        public async Task<AdminReadDto?> CreateAsync(AdminCreateDto user)
        {
            var reply = await _client.CreateAsync(_mapper.Map<AdminCreateData>(user));

            return reply.Status == AccountStatus.Successfull
                ? _mapper.Map<AdminReadDto>(reply.User)
                : null;
        }

        public async Task<AdminReadDto?> UpdateAsync(string id, AdminCreateDto user)
        {
            var reply = await _client.UpdateAsync(new AdminUpdateData
            {
                Id = id,
                User = _mapper.Map<AdminCreateData>(user)
            });

            return reply.Status == AccountStatus.Successfull
                ? _mapper.Map<AdminReadDto>(reply.User)
                : null;
        }

        public async Task<AdminReadDto?> DeleteAsync(string id)
        {
            var reply = await _client.DeleteAsync(new AdminIdData
            {
                Id = id
            });

            return reply.Status == AccountStatus.Successfull
                ? _mapper.Map<AdminReadDto>(reply.User)
                : null;
        }

        public void Dispose()
        {
            _channel.Dispose();
        }
    }
}
