using Microsoft.AspNetCore.Http;

namespace NoahStore.Core.Dto
{
    public sealed record AddProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Photos { get; set; }
    }
}
