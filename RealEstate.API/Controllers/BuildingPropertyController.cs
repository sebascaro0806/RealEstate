﻿using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Filters;
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
        public async Task<IActionResult> GetProperties([FromQuery] BuildingPropertyFilterDTO buildingPropertyFilter)
        {
            return Ok(await _buildingPropertyService.GetBuildingProperties(buildingPropertyFilter));
        }

        /// <summary>
        /// Creates a new building property.
        /// </summary>
        /// <param name="propertyDTO">The data for the new building property.</param>
        [HttpPost]
        public async Task<IActionResult> CreateProperty(CreateBuildingPropertyDTO propertyDTO)
        {
            var result = await _buildingPropertyService.CreateBuildingProperty(propertyDTO);
            return Created(result.Id.ToString(), result);
        }

        /// <summary>
        /// Adds an image to a building property.
        /// </summary>
        /// <param name="propertyId">The ID of the building property.</param>
        /// <param name="file">The image file to be added.</param>
        [HttpPost("{propertyId}/images")]
        [ValidateFile(10485760, ".jpg", ".png")]
        [ValidateGuidIdAttribute("propertyId")]
        public async Task<IActionResult> AddPropertyImage(string propertyId, IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                await _buildingPropertyService.AddImageToBuildingProperty(propertyId, file.FileName, stream);
                return Created(propertyId.ToString(), file.FileName);
            }
        }

        /// <summary>
        /// Changes the price of a building property.
        /// </summary>
        /// <param name="propertyId">The ID of the building property.</param>
        /// <param name="price">The new price of the building property.</param>
        [HttpPut("{propertyId}/price")]
        [ValidateGuidIdAttribute("propertyId")]
        public async Task<IActionResult> ChangePropertyPrice(string propertyId, double price)
        {
            await _buildingPropertyService.ChangeBuildingPropertyPrice(propertyId, price);
            return NoContent();
        }

        /// <summary>
        /// Updates a building property.
        /// </summary>
        /// <param name="propertyId">The ID of the building property.</param>
        /// <param name="propertyDTO">The updated data for the building property.</param>
        [HttpPut("{propertyId}")]
        [ValidateGuidIdAttribute("propertyId")]
        public async Task<IActionResult> UpdateBuildingProperty(string propertyId, UpdateBuildingPropertyDTO propertyDTO)
        {
            await _buildingPropertyService.UpdateBuildingProperty(propertyId, propertyDTO);
            return NoContent();
        }
    }
}
