namespace NoahStore.Core.Dto
{
    public class OrderDto
    {
        public int DeliveryMethodId { get; set; }
        public string BasketId { get; set; }
        public ShippingAddressDto shippingAddressDto { get; set; }

    }
}
