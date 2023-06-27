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

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerController"/> class.
        /// </summary>
        /// <param name="ownerService">The service for managing owners.</param>
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        /// <summary>
        /// Creates a new owner.
        /// </summary>
        /// <param name="ownerDTO">The data for the new owner.</param>
        [HttpPost]
        public async Task<IActionResult> CreateOwner(CreateOwnerDTO ownerDTO)
        {
            return Ok(await _ownerService.CreateOwner(ownerDTO));
        }

        /// <summary>
        /// Retrieves all owners.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetOwners()
        {
            return Ok(await _ownerService.GetOwners());
        }

    }
}
