using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.BuildingProperty;
using RealEstate.Application.Interfaces;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingPropertyController : ControllerBase
    {

        private readonly IBuildingPropertyService _buildingPropertyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingPropertyController"/> class.
        /// </summary>
        /// <param name="buildingPropertyService">The service for managing building properties.</param>
        public BuildingPropertyController(IBuildingPropertyService buildingPropertyService)
        {
            _buildingPropertyService = buildingPropertyService;
        }

        /// <summary>
        /// Retrieves all building properties.
        /// </summary>
        /// <param name="filterDTO">The filter query string parameter.</param>
        /// <returns>An <see cref="IActionResult"/> containing the building properties.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProperties([FromQuery] String filterDTO)
        {
            return Ok(await _buildingPropertyService.GetBuildingProperties());
        }

        /// <summary>
        /// Creates a new building property.
        /// </summary>
        /// <param name="propertyDTO">The data for the new building property.</param>
        [HttpPost]
        public async Task<IActionResult> CreateProperty(CreateBuildingPropertyDTO propertyDTO)
        {
            return Ok(await _buildingPropertyService.CreateBuildingProperty(propertyDTO));
        }

        /// <summary>
        /// Adds an image to a building property.
        /// </summary>
        /// <param name="propertyId">The ID of the building property.</param>
        /// <param name="file">The image file to be added.</param>
        [HttpPost("{propertyId}/images")]
        public async Task<IActionResult> AddPropertyImage(Guid propertyId, IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file has been sent");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var imageData = stream.ToArray();

                await _buildingPropertyService.AddImageToBuildingProperty(propertyId, imageData);
                return Ok();
            }
        }

        /// <summary>
        /// Changes the price of a building property.
        /// </summary>
        /// <param name="propertyId">The ID of the building property.</param>
        /// <param name="price">The new price of the building property.</param>
        [HttpPut("{propertyId}/price")]
        public async Task<IActionResult> ChangePropertyPrice(Guid propertyId, double price)
        {
            await _buildingPropertyService.ChangeBuildingPropertyPrice(propertyId, price);
            return Ok();
        }

        /// <summary>
        /// Updates a building property.
        /// </summary>
        /// <param name="propertyId">The ID of the building property.</param>
        /// <param name="propertyDTO">The updated data for the building property.</param>
        [HttpPut("{propertyId}")]
        public async Task<IActionResult> UpdateBuildingProperty(Guid propertyId, UpdateBuildingPropertyDTO propertyDTO)
        {
            await _buildingPropertyService.UpdateBuildingProperty(propertyId, propertyDTO);
            return Ok();
        }
    }
}
