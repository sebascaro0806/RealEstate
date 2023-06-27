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

        public BuildingPropertyController(IBuildingPropertyService buildingPropertyService)
        {
            _buildingPropertyService = buildingPropertyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(CreateBuildingPropertyDTO propertyDTO)
        {
            return Ok(await _buildingPropertyService.CreateBuildingProperty(propertyDTO));
        }

        [HttpPost("{propertyId}/images")]
        public IActionResult AddPropertyImage(Guid propertyId, [FromBody] String imageDTO)
        {
            return Ok("Image added successfully");
        }

        [HttpPut("{propertyId}/price")]
        public IActionResult ChangePropertyPrice(Guid propertyId, decimal price)
        {
            return Ok("Price changed successfully");
        }

        [HttpPut("{propertyId}")]
        public IActionResult UpdateProperty(Guid propertyId, [FromBody] String propertyDTO)
        {
            return Ok("Property updated successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetProperties([FromQuery] String filterDTO)
        {
            return Ok(await _buildingPropertyService.GetBuildingProperties());
        }
    }
}
