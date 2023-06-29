using AutoMapper;
using RealEstate.Application.DTOs.BuildingProperty;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;
using RealEstate.Infrastructure.ExternalServices.Storage;

namespace RealEstate.Application.Interfaces
{
    /// <summary>
    /// Represents a service for managing building property entities.
    /// </summary>
    public class BuildingPropertyService : IBuildingPropertyService
    {
        private readonly IBuildingPropertyRepository _buildingPropertyRepository;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingPropertyService"/> class.
        /// </summary>
        /// <param name="buildingPropertyRepository">The building property repository.</param>
        /// <param name="mapper">The mapper.</param>
        public BuildingPropertyService(IBuildingPropertyRepository buildingPropertyRepository, IStorageService storageService, IMapper mapper)
        {
            _buildingPropertyRepository = buildingPropertyRepository;
            _storageService = storageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all building properties.
        /// </summary>
        /// <returns>A collection of building property DTOs.</returns>
        public async Task<IEnumerable<BuildingPropertyDTO>> GetBuildingProperties()
        {
            List<BuildingProperty> buildingProperties = (await _buildingPropertyRepository.GetBuildingProperties()).ToList();
            return _mapper.Map<List<BuildingPropertyDTO>>(buildingProperties);
        }

        /// <summary>
        /// Creates a new building property.
        /// </summary>
        /// <param name="buildingPropertyDTO">The building property DTO.</param>
        /// <returns>The created building property DTO.</returns>
        public async Task<BuildingPropertyDTO> CreateBuildingProperty(CreateBuildingPropertyDTO buildingPropertyDTO)
        {
            BuildingProperty buildingProperty = _mapper.Map<BuildingProperty>(buildingPropertyDTO);
            await _buildingPropertyRepository.CreateBuildingProperty(buildingProperty);
            return _mapper.Map<BuildingPropertyDTO>(buildingProperty);
        }

        /// <summary>
        /// Adds an image to a building property.
        /// </summary>
        /// <param name="propertyId">The property ID.</param>
        /// <param name="imageData">The image data.</param>
        public async Task AddImageToBuildingProperty(Guid propertyId, string filename, Stream stream)
        {
            BuildingProperty property = await _buildingPropertyRepository.GetBuildingPropertyById(propertyId);
            string url =await _storageService.UploadFileAsync("images", filename, stream);

            if (property.BuildingPropertiesImages == null)
            {
                property.BuildingPropertiesImages = new List<BuildingPropertyImage>();
            }
            else
            {
                if (!property.BuildingPropertiesImages.Any(i => IsImageEqual(i, url)))
                {
                    property.BuildingPropertiesImages.Add(new BuildingPropertyImage
                    {
                        Enabled = true,
                        Url = url
                    });
                }
            }

            await _buildingPropertyRepository.UpdateBuildingProperty(property);
        }

        /// <summary>
        /// Changes the price of a building property.
        /// </summary>
        /// <param name="propertyId">The property ID.</param>
        /// <param name="price">The new price.</param>
        public async Task ChangeBuildingPropertyPrice(Guid propertyId, double price)
        {
            BuildingProperty property = await _buildingPropertyRepository.GetBuildingPropertyById(propertyId);
            property.Price = price;
            await _buildingPropertyRepository.UpdateBuildingProperty(property);
        }

        /// <summary>
        /// Updates the details of a building property.
        /// </summary>
        /// <param name="propertyId">The property ID.</param>
        /// <param name="buildingPropertyDTO">The updated building property DTO.</param>
        public async Task UpdateBuildingProperty(Guid propertyId, UpdateBuildingPropertyDTO buildingPropertyDTO)
        {
            BuildingProperty property = await _buildingPropertyRepository.GetBuildingPropertyById(propertyId);

            property.Name = buildingPropertyDTO.Name;
            property.Address = buildingPropertyDTO.Address;
            property.CodeInternal= buildingPropertyDTO.CodeInternal;
            property.Price = buildingPropertyDTO.Price;
            property.Year = buildingPropertyDTO.Year;

            await _buildingPropertyRepository.UpdateBuildingProperty(property);
        }

        /// <summary>
        /// Compares the URL of an image with a provided URL and returns a boolean value indicating whether they are equal.
        /// </summary>
        /// <param name="image">The image to compare.</param>
        /// <param name="url">The URL to compare the image with.</param>
        /// <returns>A boolean value indicating whether the image URL is equal to the provided URL.</returns>
        private bool IsImageEqual(BuildingPropertyImage image, string url)
        {
            return string.Equals(image.Url, url, StringComparison.OrdinalIgnoreCase);
        }
    }
}
