using App.Models;
using AutoMapper;
using Business.Models;

namespace App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
        }
    }
}
