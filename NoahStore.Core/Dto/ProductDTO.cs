namespace NoahStore.API.DTOs
{
    public sealed record ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryName { get; set; }
        public ICollection<ProductImageDTO> Images { get; set; }
    }
}
