using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly RealEstateDBConext _context;

        public OwnerRepository(RealEstateDBConext context)
        {
            _context = context;
        }

        public async Task CreateOwner(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Owner>> GetOwners()
        {
            return await _context.Owners.ToListAsync();
        }
    }
}
