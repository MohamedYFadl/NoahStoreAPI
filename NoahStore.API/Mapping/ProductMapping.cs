using AutoMapper;
using NoahStore.API.DTOs;
using NoahStore.Core.Entities;

namespace NoahStore.API.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductImage, ProductImageDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>()
                .ForMember(p => p.CategoryName,
                op => op.MapFrom(src => src.Category.Name))
                .ForMember(p => p.Images,
                op => op.MapFrom(src => src.Images))
                .ReverseMap();
        }
    }
}
