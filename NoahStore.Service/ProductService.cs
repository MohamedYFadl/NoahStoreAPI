using AutoMapper;
using NoahStore.Core.Dto;
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
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ProductService(
            IGenericRepository<Product> productRepo,
            IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            IMapper mapper,
            IImageService imageService)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
            _imageService = imageService;
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

        public async Task<AddProductDTO> AddProductAsync(AddProductDTO addProductDTO)
        {
            var mappedProduct = _mapper.Map<Product>(addProductDTO);
           var product =  await  _productRepository.AddProductAsync(mappedProduct);
            List<ProductImage> images;
            List<string> ImagePath;
            if (product is not null)
            {
                if(addProductDTO.Photos != null || addProductDTO.Photos.Count > 0)
                {
                 ImagePath = await _imageService.AddImageAsync(addProductDTO.Photos, addProductDTO.Name);
                }
                else
                {
                    ImagePath = ["Images/no-image.jpg"];

                }
                    images = ImagePath.Select(path => new ProductImage
                    {
                        ImageURL = path,
                        ProductId = product.Id,
                        AltText = product.Name,
                        CreatedAt = DateTime.Now,
                        FileSize = path.Length * 1024 * 1024,
                        Caption = product.Description,
                    }).ToList();
                await _unitOfWork.Repository<ProductImage>().AddRangeAsync(images);
                _unitOfWork.Save();
            }
            return addProductDTO;
        }

    }
}
