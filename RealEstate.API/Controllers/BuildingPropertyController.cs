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
            // Lógica para crear un nuevo edificio de propiedades
            // Utiliza el objeto PropertyDTO para obtener los datos de la propiedad
            // Llama al servicio correspondiente para realizar la operación

            // Ejemplo de respuesta
            return Ok("Property created successfully Sebastuian Ca");
        }

        [HttpPost("{propertyId}/images")]
        public IActionResult AddPropertyImage(int propertyId, [FromBody] String imageDTO)
        {
            // Lógica para agregar una imagen a una propiedad existente
            // Utiliza el identificador de propiedad (propertyId) para asociar la imagen a la propiedad correspondiente
            // Utiliza el objeto ImageDTO para obtener los datos de la imagen
            // Llama al servicio correspondiente para realizar la operación

            // Ejemplo de respuesta
            return Ok("Image added successfully");
        }

        [HttpPut("{propertyId}/price")]
        public IActionResult ChangePropertyPrice(int propertyId, [FromBody] String priceDTO)
        {
            // Lógica para cambiar el precio de una propiedad existente
            // Utiliza el identificador de propiedad (propertyId) para identificar la propiedad correspondiente
            // Utiliza el objeto PriceDTO para obtener el nuevo precio
            // Llama al servicio correspondiente para realizar la operación

            // Ejemplo de respuesta
            return Ok("Price changed successfully");
        }

        [HttpPut("{propertyId}")]
        public IActionResult UpdateProperty(int propertyId, [FromBody] String propertyDTO)
        {
            // Lógica para actualizar los detalles de una propiedad existente
            // Utiliza el identificador de propiedad (propertyId) para identificar la propiedad correspondiente
            // Utiliza el objeto PropertyDTO para obtener los nuevos datos de la propiedad
            // Llama al servicio correspondiente para realizar la operación

            // Ejemplo de respuesta
            return Ok("Property updated successfully");
        }

        [HttpGet]
        public IActionResult GetProperties([FromQuery] String filterDTO)
        {
            return Ok(_buildingPropertyService.GetBuildingProperties());
        }
    }
}
