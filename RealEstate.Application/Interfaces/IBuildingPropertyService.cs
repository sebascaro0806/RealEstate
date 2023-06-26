using RealEstate.Domain.Models;

namespace RealEstate.Application.Interfaces
{
    public interface IBuildingPropertyService
    {
        IEnumerable<BuildingProperty> GetBuildingProperties();
    }
}
