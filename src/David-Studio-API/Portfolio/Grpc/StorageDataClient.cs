using AutoMapper;
using Google.Protobuf;
using Grpc.Net.Client;
using Portfolio;
using Portfolio.Dtos;

namespace Portfolio.Grpc
{
    public class StorageDataClient : IStorageDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private readonly string? uri;

        public StorageDataClient(
            IConfiguration configuration,
            IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;

            uri = _configuration.GetValue<string>("Services:StorageUri");
            if (uri is null) throw new Exception("Storage service uri is null");
        }

        public async Task<ImageReadDto> StoreImageAsync(IFormFile file)
        {
            using var channel = GrpcChannel.ForAddress(uri!);
            var client = new Storage.StorageClient(channel);

            var call = client.StoreImage();

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);

                await call.RequestStream.WriteAsync(new ImageRequest
                {
                    FileName = file.FileName,
                    File = ByteString.CopyFrom(ms.ToArray())
                });
            }

            await call.RequestStream.CompleteAsync();

            ImageResponse response = await call.ResponseAsync;

            return _mapper.Map<ImageReadDto>(response.Image);
        }
    }
}
