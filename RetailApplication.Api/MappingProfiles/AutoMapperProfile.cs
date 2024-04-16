using AutoMapper;
using RetailApplication.Core.Entities;

namespace RetailApplication.Api.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductApproval, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductPrice));
        }
    }
}
