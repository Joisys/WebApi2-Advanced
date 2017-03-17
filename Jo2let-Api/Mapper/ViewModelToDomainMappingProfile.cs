using AutoMapper;
using Jo2let.Api.Models.Location;
using Jo2let.Api.Models.Property;
using Jo2let.Model;

namespace Jo2let.Api.Mapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<LocationViewModel, Location>();
            CreateMap<CreateLocationViewModel, Location>();
            CreateMap<EditLocationViewModel, Location>();

            CreateMap<PropertyViewModel, Property>();
            CreateMap<CreatePropertyViewModel, Property>();
            CreateMap<EditPropertyViewModel, Property>();
        }
    }
}