using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Owner;
using RealEstate.Application.Interfaces;

namespace RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner(CreateOwnerDTO ownerDTO)
        {
            return Ok(await _ownerService.CreateOwner(ownerDTO));
        }

        [HttpGet]
        public async Task<IActionResult> GetOwners()
        {
            return Ok(await _ownerService.GetOwners());
        }

    }
}
