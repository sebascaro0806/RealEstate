using RealEstate.Domain.Models;

namespace RealEstate.Domain.Interfaces
{
    public interface IOwnerRepository
    {
        public Task CreateOwner(Owner owner);
        public Task<IEnumerable<Owner>> GetOwners();
    }
}
