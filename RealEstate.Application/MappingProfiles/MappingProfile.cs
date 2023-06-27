using AutoMapper;
using RealEstate.Application.DTOs.BuildingProperty;
using RealEstate.Application.DTOs.Owner;
using RealEstate.Domain.Models;

namespace RealEstate.Application.MappingProfiles
{
    public  class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDTO>();
            CreateMap<OwnerDTO, Owner>();

            CreateMap<CreateOwnerDTO, Owner>();

            CreateMap<BuildingProperty, BuildingPropertyDTO>();
            CreateMap<BuildingPropertyDTO, BuildingProperty>();

            CreateMap<CreateBuildingPropertyDTO, BuildingProperty>();
            CreateMap<UpdateBuildingPropertyDTO, BuildingProperty>();
        }
    }
}
