using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateProperty([FromBody] String propertyDTO)
        {
            return Ok("Property created successfully");
        }

        [HttpPost("{propertyId}/images")]
        public IActionResult AddPropertyImage(int propertyId, [FromBody] String imageDTO)
        {
            return Ok("Image added successfully");
        }

        [HttpPut("{propertyId}/price")]
        public IActionResult ChangePropertyPrice(int propertyId, [FromBody] String priceDTO)
        {
            return Ok("Price changed successfully");
        }

        [HttpPut("{propertyId}")]
        public IActionResult UpdateProperty(int propertyId, [FromBody] String propertyDTO)
        {
            return Ok("Property updated successfully");
        }

        [HttpGet]
        public IActionResult GetProperties([FromQuery] String filterDTO)
        {
            return Ok(_buildingPropertyService.GetBuildingProperties());
        }
    }
}
