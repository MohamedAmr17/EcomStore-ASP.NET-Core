using AutoMapper;
using Ecom.DTOS;
using Ecom.Models;

namespace Ecom.Models
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}