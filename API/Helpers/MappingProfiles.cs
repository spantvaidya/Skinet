using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.ProductBrand, x => x.MapFrom(s => s.ProductBrand.Name))
            .ForMember(dest => dest.ProductType, x => x.MapFrom(s => s.ProductType.Name))
            .ForMember(dest => dest.PictureUrl, x => x.MapFrom<ProductUrlResolver>())
            .ReverseMap();

        }
    }
}