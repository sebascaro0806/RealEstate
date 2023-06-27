using RealEstate.Domain.Models;

namespace RealEstate.Domain.Interfaces
{
    public interface IBuildingPropertyRepository
    {
        public Task CreateBuildingProperty(BuildingProperty buildingProperty);
        public Task<IEnumerable<BuildingProperty>> GetBuildingProperties();
    }
}
