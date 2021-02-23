using ApplicationShared.DTOs;
using ApplicationShared.DTOs.Product;
using AutoMapper;
using Warehouse.DomainModels.Models;

namespace Warehouse.ApplicationService.Mappers
{
    public class ObjectsMapper : Profile
    {
        public ObjectsMapper()
        {
            CreateMap<User, UserDetailDto>().ReverseMap();
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserRegisterDto, UserDetailDto>();
            //product
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductDetail>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
