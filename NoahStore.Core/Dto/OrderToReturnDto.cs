using NoahStore.Core.Entities.Order;

namespace NoahStore.Core.Dto
{
    public sealed record OrderToReturnDto
    {
        public int Id { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public ShippingAddressDto ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItemsDTO> OrderItems { get; set; }

        public string OrderStatus { get; set; } 
        public string PaymentStatus { get; set; } 
        public DateTime OrderDate { get; set; }
    }
}
