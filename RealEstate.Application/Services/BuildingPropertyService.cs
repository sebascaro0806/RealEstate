using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;

namespace RealEstate.Application.Interfaces
{
    public class BuildingPropertyService : IBuildingPropertyService
    {
        private readonly IBuildingPropertyRepository _buildingPropertyRepository;

        public BuildingPropertyService(IBuildingPropertyRepository buildingPropertyRepository)
        {
            _buildingPropertyRepository = buildingPropertyRepository;
        }


        IEnumerable<BuildingProperty> IBuildingPropertyService.GetBuildingProperties()
        {
            return _buildingPropertyRepository.GetBuildingProperties();
        }
    }
}
