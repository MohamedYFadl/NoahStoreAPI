using NoahStore.Core.Dto;
using NoahStore.Core.Entities;

namespace NoahStore.Core.Interfaces
{
    public interface IProductRepository
    {
     
        Task<Product> AddProductAsync(Product product);
        public string GenerateSKU(Product productDTO);
    }
}
