namespace NoahStore.Core.Dto
{
    public sealed record OrderItemsDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string MainImage { get; set; }
    }
}
