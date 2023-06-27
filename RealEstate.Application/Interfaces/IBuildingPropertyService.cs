using RealEstate.Application.DTOs.BuildingProperty;
using RealEstate.Domain.Models;

namespace RealEstate.Application.Interfaces
{
    public interface IBuildingPropertyService
    {
        public Task<BuildingPropertyDTO> CreateBuildingProperty(CreateBuildingPropertyDTO buildingPropertyDTO);
        public Task<IEnumerable<BuildingPropertyDTO>> GetBuildingProperties();
    }
}
