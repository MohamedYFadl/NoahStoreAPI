using AutoMapper;
using NoahStore.Core.Dto;
using NoahStore.Core.Entities.Order;

namespace NoahStore.API.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();
            CreateMap<Orders,OrderToReturnDto>()
                .ForMember(d=>d.OrderDate,O=>O.MapFrom(S=>S.CreatedAt))
                .ForMember(d=>d.DeliveryMethod,O=>O.MapFrom(s=>s.DeliveryMethod.Name))
                .ReverseMap();
            CreateMap<OrderItems,OrderItemsDTO>().ReverseMap();
        }
    }
}
