using AutoMapper;
using ModelWebApi.Domain.Entities;
using PruebaAnnarApi.Application.Dto.User;

namespace PruebaAnnarApi.Application.Mapper
{
    public class GlobalMapperProfile: Profile
    {
        public GlobalMapperProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserListDto>();
        }
    }
}
