using NoahStore.Core.Entities;
using NoahStore.Core.Sharing;

namespace NoahStore.Core.Services
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecsParams specsParams);
        Task<Product> GetProductByIdAsync(int productId);
        Task<int> GetCountAsync(ProductSpecsParams specsParams);
    }
}
