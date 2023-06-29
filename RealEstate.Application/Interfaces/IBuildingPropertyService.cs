using RealEstate.Application.DTOs.BuildingProperty;

namespace RealEstate.Application.Interfaces
{
    /// <summary>
    /// Represents a service interface for managing building properties.
    /// </summary>
    public interface IBuildingPropertyService
    {
        /// <summary>
        /// Retrieves a collection of building properties.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of building properties.</returns>
        public Task<IEnumerable<BuildingPropertyDTO>> GetBuildingProperties(BuildingPropertyFilterDTO buildingPropertyFilter);

        /// <summary>
        /// Creates a new building property.
        /// </summary>
        /// <param name="buildingPropertyDTO">The DTO object containing the details of the building property to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created building property.</returns>
        public Task<BuildingPropertyDTO> CreateBuildingProperty(CreateBuildingPropertyDTO buildingPropertyDTO);

        /// <summary>
        /// Adds an image to a building property.
        /// </summary>
        /// <param name="propertyId">The ID of the building property.</param>
        /// <param name="imageData">The binary data of the image to add.</param>
        public Task AddImageToBuildingProperty(string propertyId, string filename, Stream stream);

        /// <summary>
        /// Changes the price of a building property.
        /// </summary>
        /// <param name="propertyId">The ID of the building property.</param>
        /// <param name="price">The new price of the building property.</param>
        public Task ChangeBuildingPropertyPrice(string propertyId, double price);

        /// <summary>
        /// Updates the details of a building property.
        /// </summary>
        /// <param name="propertyId">The ID of the building property to update.</param>
        /// <param name="buildingPropertyDTO">The DTO object containing the updated details of the building property.</param>
        public Task UpdateBuildingProperty(string propertyId, UpdateBuildingPropertyDTO buildingPropertyDTO);
    }
}
