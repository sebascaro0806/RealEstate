using RealEstate.Application.DTOs.Owner;

namespace RealEstate.Application.Interfaces
{
    public interface IOwnerService
    {
        public Task<OwnerDTO> CreateOwner(CreateOwnerDTO owner);

        public Task<IEnumerable<OwnerDTO>> GetOwners();
    }
}
