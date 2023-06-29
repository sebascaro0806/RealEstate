﻿using RealEstate.Domain.Models;

namespace RealEstate.Domain.Interfaces
{
    /// <summary>
    /// Represents a repository for managing owner entities.
    /// </summary>
    public interface IOwnerRepository
    {
        /// <summary>
        /// Creates a new owner.
        /// </summary>
        /// <param name="owner">The owner to create.</param>
        Task CreateOwner(Owner owner);

        /// <summary>
        /// Retrieves all owners.
        /// </summary>
        /// <returns>A collection of all owners.</returns>
        Task<IEnumerable<Owner>> GetOwners();
    }
}
