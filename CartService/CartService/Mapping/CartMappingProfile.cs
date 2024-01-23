using AutoMapper;
using CartService.Dtos;
using CartService.Entities;

namespace CartService.Mapping
{
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            CreateMap<CartItem, AddCartItemDto>().ReverseMap();
        }
    }
}
