using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repository
{
    /// <summary>
    /// Repository implementation for managing owners.
    /// </summary>
    public class OwnerRepository : IOwnerRepository
    {
        private readonly RealEstateDBConext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public OwnerRepository(RealEstateDBConext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all owners.
        /// </summary>
        /// <returns>A collection of owners.</returns>
        public async Task<IEnumerable<Owner>> GetOwners()
        {
            return await _context.Owners.ToListAsync();
        }

        /// <summary>
        /// Creates a new owner.
        /// </summary>
        /// <param name="owner">The owner to create.</param>
        public async Task CreateOwner(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();
        }
    }
}
