using AutoMapper;
using NoahStore.Core.Dto;
using NoahStore.Core.Entities;
using NoahStore.Core.Interfaces;
using NoahStore.Core.Services;
using NoahStore.Infrastructure.Data.DbContexts;

namespace NoahStore.Infrastructure.Repositories
{
    public class ProductRepository :GenericRepositoy<Product> ,IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ProductRepository(
            ApplicationDbContext db,
            IMapper mapper,IImageService imageService) : base(db)
        {
            _context = db;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<bool> AddProductAsync(AddProductDTO productDTO)
        {
            if (productDTO == null) return false;
            var sku = GenerateSKU(productDTO);
            var product = _mapper.Map<Product>(productDTO);
            product.SKU = sku;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            List<string> ImagePath = await _imageService.AddImageAsync(productDTO.Photos, productDTO.Name);
            List<ProductImage> images = ImagePath.Select(path => new ProductImage
            {
                ImageURL = path,
                ProductId = product.Id,
                AltText = product.Name,
                CreatedAt = DateTime.Now,

            }).ToList();
            await _context.AddRangeAsync(images);
            await _context.SaveChangesAsync();
            return true;
        }

        public string GenerateSKU(AddProductDTO productDTO)
        {
            Category category = _context.Categories.FirstOrDefault(c=>c.Id == productDTO.CategoryId);
            string baseSku = category.Name.Substring(0,3).ToUpper();

            string sku = baseSku;
            int sequence = 1;
            while(_context.Products.Any(p=>p.SKU.ToLower() == sku.ToLower()))
            {
                sku = $"{baseSku}-{sequence.ToString("D5")}";
                sequence++;
            }
            return sku;
        }
    }
}
