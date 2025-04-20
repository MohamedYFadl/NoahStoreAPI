using NoahStore.Core.Entities;
using NoahStore.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahStore.Core.Services
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecsParams specsParams);
        Task<Product> GetProductByIdAsync(int productId);
        Task<int> GetCountAsync(ProductSpecsParams specsParams);
    }
}
