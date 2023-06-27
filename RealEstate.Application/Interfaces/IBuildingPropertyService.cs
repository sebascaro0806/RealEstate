using RealEstate.Domain.Models;

namespace RealEstate.Application.Interfaces
{
    public interface IBuildingPropertyService
    {
        public Task<IEnumerable<BuildingProperty>> GetBuildingProperties();
    }
}
