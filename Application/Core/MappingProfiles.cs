using Application.Categories;
using Application.Clients;
using Application.Manufacturers;
using Application.Products;
using Application.Users;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Client, Client>();
            CreateMap<Client, ClientDto>();

            CreateMap<Manufacturer, Manufacturer>();
            CreateMap<Manufacturer, ManufacturerDto>();

            CreateMap<Category, Category>();
            CreateMap<Category, CategoryDto>();

            CreateMap<Product, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<AppUser, AppUser>();
            CreateMap<AppUser, UserDto>();
        }
    }
}