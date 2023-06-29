using RealEstate.Domain.Filters;
using RealEstate.Domain.Models;

namespace RealEstate.Domain.Interfaces
{
    /// <summary>
    /// Represents the repository interface for managing building properties.
    /// </summary>
    public interface IBuildingPropertyRepository
    {
        /// <summary>
        /// Retrieves a building property by its unique identifier.
        /// </summary>
        /// <param name="propertyId">The unique identifier of the building property.</param>
        /// <returns>The building property with the specified identifier, or null if not found.</returns>
        Task<BuildingProperty> GetBuildingPropertyById(Guid propertyId);

        /// <summary>
        /// Retrieves all building properties.
        /// </summary>
        /// <returns>A collection of all building properties.</returns>
        Task<IEnumerable<BuildingProperty>> GetBuildingProperties(BuildingPropertyFilter buildingPropertyFilter);

        /// <summary>
        /// Updates a building property.
        /// </summary>
        /// <param name="buildingProperty">The building property to update.</param>
        Task UpdateBuildingProperty(BuildingProperty buildingProperty);

        /// <summary>
        /// Creates a new building property.
        /// </summary>
        /// <param name="buildingProperty">The building property to create.</param>
        Task CreateBuildingProperty(BuildingProperty buildingProperty);
    }
}
