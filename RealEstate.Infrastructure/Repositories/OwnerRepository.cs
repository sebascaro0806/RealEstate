using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing owners.
    /// </summary>
    public class OwnerRepository : IOwnerRepository
    {
        private readonly RealEstateDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public OwnerRepository(RealEstateDBContext context)
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
        /// Retrieves owner by id.
        /// </summary>
        /// <param name="ownerId">The ownerId.</param>
        /// <returns>A collection of owners.</returns>
        public async Task<Owner> GetOwnerById(Guid ownerId)
        {
            var owner = await _context.Owners
                .FirstOrDefaultAsync(bp => bp.Id == ownerId);

            if (owner == null)
            {
                throw new NotFoundException($"The owner with the ID {ownerId} does not exist.");
            }

            return owner;
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
