using Portfolio.Dtos;

namespace Portfolio.Grpc
{
    public interface IStorageDataClient
    {
        Task<ImageReadDto> StoreImageAsync(IFormFile file);
    }
}
