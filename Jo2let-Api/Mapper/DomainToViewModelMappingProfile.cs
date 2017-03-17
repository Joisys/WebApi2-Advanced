using AutoMapper;
using AutoMapper.XpressionMapper;
using Jo2let.Api.Models.Location;
using Jo2let.Api.Models.Property;
using Jo2let.Model;

namespace Jo2let.Api.Mapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Location, LocationViewModel>();
            CreateMap<Property, PropertyViewModel>();
        }
    }
}