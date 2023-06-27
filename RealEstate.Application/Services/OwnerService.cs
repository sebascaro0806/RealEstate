using AutoMapper;
using RealEstate.Application.DTOs.Owner;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;

namespace RealEstate.Application.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerService(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<OwnerDTO> CreateOwner(CreateOwnerDTO ownerDTO)
        {
            Owner owner = _mapper.Map<Owner>(ownerDTO);
            await _ownerRepository.CreateOwner(owner);
            return _mapper.Map<OwnerDTO>(owner);
        }

        public async Task<IEnumerable<OwnerDTO>> GetOwners()
        {
            List<Owner> owners = (await _ownerRepository.GetOwners()).ToList();
            return _mapper.Map<List<OwnerDTO>>(owners);
        }
    }
}
