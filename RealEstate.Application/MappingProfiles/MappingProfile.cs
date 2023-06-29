using AutoMapper;
using RealEstate.Application.DTOs.BuildingProperty;
using RealEstate.Application.DTOs.Owner;
using RealEstate.Domain.Filters;
using RealEstate.Domain.Models;

namespace RealEstate.Application.MappingProfiles
{

    /// <summary>
    /// Represents a mapping profile for AutoMapper.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDTO>();
            CreateMap<OwnerDTO, Owner>();

            CreateMap<CreateOwnerDTO, Owner>();

            CreateMap<BuildingProperty, BuildingPropertyDTO>();
            CreateMap<BuildingPropertyDTO, BuildingProperty>();
            CreateMap<BuildingPropertyImage, BuildingPropertyImageDTO>();

            CreateMap<CreateBuildingPropertyDTO, BuildingProperty>();
            CreateMap<UpdateBuildingPropertyDTO, BuildingProperty>();

            CreateMap<BuildingPropertyFilterDTO, BuildingPropertyFilter>();
        }
    }
}
