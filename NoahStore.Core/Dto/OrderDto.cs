namespace NoahStore.Core.Dto
{
    public sealed record OrderDto
    {
        public int DeliveryMethodId { get; set; }
        public string BasketId { get; set; }
        public ShippingAddressDto shippingAddressDto { get; set; }

    }
}
