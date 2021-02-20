using ApplicationShared.DTOs;
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
        }
    }
}
