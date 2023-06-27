using RealEstate.Application.DTOs.Owner;

namespace RealEstate.Application.Interfaces
{
    /// <summary>
    /// Represents a service interface for managing owners.
    /// </summary>
    public interface IOwnerService
    {
        /// <summary>
        /// Creates a new owner.
        /// </summary>
        /// <param name="owner">The DTO object containing the details of the owner to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created owner.</returns>
        public Task<OwnerDTO> CreateOwner(CreateOwnerDTO owner);

        /// <summary>
        /// Retrieves a collection of owners.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of owners.</returns>
        public Task<IEnumerable<OwnerDTO>> GetOwners();
    }
}
