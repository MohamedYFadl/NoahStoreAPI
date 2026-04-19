using System.ComponentModel.DataAnnotations;

namespace NoahStore.Core.Dto
{
    public sealed record OrderDto
    {
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public ShippingAddressDto shippingAddressDto { get; set; }

    }
}
