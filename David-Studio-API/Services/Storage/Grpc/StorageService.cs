﻿using AutoMapper;
using Grpc.Core;
using Storage.Models;
using Storage.Services;

namespace Storage.Grpc
{
    public class StorageService : Storage.StorageBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public StorageService(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public override async Task<ImageResponse> StoreImage(IAsyncStreamReader<ImageRequest> requestStream, ServerCallContext context)
        {
            Image? image = new();

            await foreach (ImageRequest request in requestStream.ReadAllAsync())
            {
                byte[] fileBytes = request.File.ToByteArray();

                using var stream = new MemoryStream(fileBytes);

                IFormFile file = new FormFile(stream, 0, fileBytes.Length, "name", request.FileName);

                image = await _repositoryManager.Images.UploadAsync(file);
                await _repositoryManager.SaveAsync();
            }

            return new ImageResponse { Image = _mapper.Map<GrpcImageModel>(image) };
        }
    }
}
