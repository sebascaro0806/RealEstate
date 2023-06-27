using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repository
{
    /// <summary>
    /// Repository implementation for managing building properties.
    /// </summary>
    public class BuildingPropertyRepository : IBuildingPropertyRepository
    {
        private readonly RealEstateDBConext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingPropertyRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public BuildingPropertyRepository(RealEstateDBConext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a building property by its ID.
        /// </summary>
        /// <param name="propertyId">The ID of the building property.</param>
        /// <returns>The building property with the specified ID.</returns>
        public async Task<BuildingProperty> GetBuildingPropertyById(Guid propertyId)
        {
            var buildingProperty = await _context.BuildingProperties.FirstOrDefaultAsync(bp => bp.Id == propertyId);
            if (buildingProperty == null)
            {
                //TODO
                throw new Exception("La propiedad no existe");
            }

            return buildingProperty;
        }

        /// <summary>
        /// Retrieves all building properties.
        /// </summary>
        /// <returns>A collection of building properties.</returns>
        public async Task<IEnumerable<BuildingProperty>> GetBuildingProperties()
        {
            return await _context.BuildingProperties.ToListAsync();
        }

        /// <summary>
        /// Creates a new building property.
        /// </summary>
        /// <param name="buildingProperty">The building property to create.</param>
        public async Task CreateBuildingProperty(BuildingProperty buildingProperty)
        {
            await _context.BuildingProperties.AddAsync(buildingProperty);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing building property.
        /// </summary>
        /// <param name="buildingProperty">The building property to update.</param>
        public async Task UpdateBuildingProperty(BuildingProperty buildingProperty)
        {
            _context.BuildingProperties.Update(buildingProperty);
            await _context.SaveChangesAsync();
        }
    }
}
