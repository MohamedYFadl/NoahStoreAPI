using NoahStore.Core.Dto;
using NoahStore.Core.Entities;

namespace NoahStore.Core.Interfaces
{
    public interface IProductRepository
    {
     
        Task<bool> AddProductAsync(AddProductDTO productDTO);
        public string GenerateSKU(AddProductDTO productDTO);
    }
}
