using AutoMapper;
using Electo.DAL.Entities;
using Electo.PL.Models;

namespace Electo.PL.Mapper
{
    public class MapperDomain : Profile
    {
        public MapperDomain()
        {
            CreateMap<Category,CategoryVM> ().ReverseMap();
            CreateMap<Brand, BrandVM>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<Cart, CartVM>().ReverseMap();
            CreateMap<Order, OrderVM>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailVM>().ReverseMap();
        }
    }
}
