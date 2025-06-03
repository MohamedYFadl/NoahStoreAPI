using NoahStore.Core.Entities;
using NoahStore.Core.Interfaces;
using NoahStore.Core.Services;
using NoahStore.Core.Sharing;
using NoahStore.Core.Specifications;

namespace NoahStore.Service
{
    public class ProductService:IProductService
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(
            IGenericRepository<Product> productRepo,IUnitOfWork unitOfWork)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            Product product =await _productRepo.GetByIdAsync(productId);
            if(product == null || product.IsDeleted == true) return false;

            product.IsDeleted = true;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepo.UpdateAsync(product);
            _unitOfWork.Save();
            return true;
        }

        public Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductSpecsParams specsParams)
        {
            var specs = new ProductWithImagesAndCategory(specsParams);
            var products = _productRepo.GetAllAsync(specs);

            return products;
        }

        public async Task<int> GetCountAsync(ProductSpecsParams specsParams)
        {
            var countspecs = new FilteredProductsForCountSpecs(specsParams);
           return await _productRepo.GetCountAsync(countspecs);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var specs = new ProductWithImagesAndCategory(productId);
            var product = await _productRepo.GetByIdAsync(productId);
            return product;
        }



    }
}
