using Microsoft.AspNetCore.Http;

namespace NoahStore.Core.Services
{
    public interface IImageService
    {
        Task<List<string>> AddImageAsync(IFormFileCollection formFiles, string productName);
        void DeleteImageAsync(string imagePath);
    }
}
