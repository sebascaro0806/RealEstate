using RealEstate.Domain.Models;

namespace RealEstate.Domain.Interfaces
{
    public interface IBuildingPropertyRepository
    {
        IEnumerable<BuildingProperty> GetBuildingProperties();
    }
}
