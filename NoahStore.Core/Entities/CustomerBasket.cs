namespace NoahStore.Core.Entities
{
    public class CustomerBasket
    {
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
        public decimal Total => BasketItems.Sum(item => item.Subtotal);
        public int TotalItems => BasketItems.Sum(item=>item.Quantity);
        public CustomerBasket()
        {
            
        }
        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
