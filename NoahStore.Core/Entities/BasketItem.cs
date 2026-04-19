namespace NoahStore.Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Category { get; set; }
        public string PictureUrl { get; set; }
        public decimal Subtotal => UnitPrice * Quantity;
    }
}