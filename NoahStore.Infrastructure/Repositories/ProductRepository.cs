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
        public ProductRepository(
            ApplicationDbContext db,
            IMapper mapper,
            IImageService imageService) : base(db)
        {
            _context = db;
        }
        public async Task<Product> AddProductAsync(Product model)
        {
            if (model == null) return null;
            model.SKU = GenerateSKU(model);
            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
            
            
        }

        public string GenerateSKU(Product model)
        {
            Category category = _context.Categories.FirstOrDefault(c=>c.Id == model.CategoryId);
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
