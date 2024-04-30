using Application.Clients;
using Application.Manufacturers;
using Application.Products;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<Client, Client>();
            CreateMap<Client, ClientDto>();

            CreateMap<Manufacturer, Manufacturer>();
            CreateMap<Manufacturer, ManufacturerDto>();
        }
    }
}