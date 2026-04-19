using AdminDashboard.ViewModels;
using AutoMapper;
using NoahStore.API.DTOs;
using NoahStore.Core.Dto;
using NoahStore.Core.Entities;

namespace AdminDashboard.Mapping
{
    public class ProductsMapping:Profile
    {
        public ProductsMapping()
        {
            CreateMap<Product,ProductsViewModel>()
                .ForMember(p=>p.CategoryName,o=>o.MapFrom(src=>src.Category.Name))
                .ForMember(p=>p.Created_Date,O=>O.MapFrom(src=>src.CreatedAt))
                .ForMember(p=>p.Name,O=>O.MapFrom(src=>src.Name))
                .ReverseMap();
            CreateMap<AddProductDTO, UpsertProductViewModel>().ReverseMap();
            CreateMap<ProductsViewModel,Product>().ReverseMap();
            CreateMap<ProductImage,ProductImageDTO>().ReverseMap();
        }
    }
}
