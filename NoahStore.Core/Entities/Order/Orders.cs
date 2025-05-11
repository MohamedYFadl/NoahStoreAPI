namespace NoahStore.Core.Entities.Order
{
    public class Orders:BaseEntity<int>
    {
        public Orders()
        {
            
        }
        public Orders(string buyerEmail, decimal subTotal, ShippingAddress shippingAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItems> orderItems)
        {
            BuyerEmail = buyerEmail;
            SubTotal = subTotal;
            this.shippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
        }

        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal SubTotal { get; set; }
        public ShippingAddress shippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItems> OrderItems { get; set; }

        public Status OrderStatus { get; set; } = Status.Pending;
        public Status PaymentStatus { get; set; } = Status.Pending;

        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Price;
    }
}
