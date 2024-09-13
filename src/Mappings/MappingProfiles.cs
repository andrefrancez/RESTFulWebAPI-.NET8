using AutoMapper;
using VehiclesAPI.Dto;
using VehiclesAPI.Models;

namespace VehiclesAPI.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CarMake, CarMakeDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Vehicle, VehicleDTO>().ReverseMap();
        }
    }
}
