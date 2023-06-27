using AutoMapper;
using RealEstate.Application.DTOs.Owner;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;

namespace RealEstate.Application.Services
{
    /// <summary>
    /// Represents a service for managing owner entities.
    /// </summary>
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerService"/> class.
        /// </summary>
        /// <param name="ownerRepository">The owner repository.</param>
        /// <param name="mapper">The mapper.</param>
        public OwnerService(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new owner.
        /// </summary>
        /// <param name="ownerDTO">The owner DTO.</param>
        /// <returns>The created owner DTO.</returns>
        public async Task<OwnerDTO> CreateOwner(CreateOwnerDTO ownerDTO)
        {
            Owner owner = _mapper.Map<Owner>(ownerDTO);
            await _ownerRepository.CreateOwner(owner);
            return _mapper.Map<OwnerDTO>(owner);
        }

        /// <summary>
        /// Retrieves all owners.
        /// </summary>
        /// <returns>A collection of owner DTOs.</returns>
        public async Task<IEnumerable<OwnerDTO>> GetOwners()
        {
            List<Owner> owners = (await _ownerRepository.GetOwners()).ToList();
            return _mapper.Map<List<OwnerDTO>>(owners);
        }
    }
}
