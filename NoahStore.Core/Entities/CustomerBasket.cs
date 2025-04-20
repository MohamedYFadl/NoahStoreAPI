namespace NoahStore.Core.Entities
{
    public class CustomerBasket:BaseEntity<string>
    {
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
        public decimal Total => BasketItems.Sum(item => item.Subtotal);
        public int TotalItems => BasketItems.Sum(item=>item.Quantity);
        public CustomerBasket()
        {
            CreatedAt = DateTime.Now;
        }
        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
