using AutoMapper;
using Ecom.DTOS;

namespace Ecom.Models
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product,ProductDto>();
            CreateMap<UserCreateDto, User>();
        }
    }
}
