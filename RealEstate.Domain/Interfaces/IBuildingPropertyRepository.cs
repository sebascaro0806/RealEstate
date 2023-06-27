using RealEstate.Domain.Models;

namespace RealEstate.Domain.Interfaces
{
    public interface IBuildingPropertyRepository
    {
        public Task<IEnumerable<BuildingProperty>> GetBuildingProperties();
    }
}
