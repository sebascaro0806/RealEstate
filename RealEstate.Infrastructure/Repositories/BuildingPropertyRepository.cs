using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Filters;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;
using RealEstate.Infrastructure.Builders;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing building properties.
    /// </summary>
    public class BuildingPropertyRepository : IBuildingPropertyRepository
    {
        private readonly RealEstateDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingPropertyRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public BuildingPropertyRepository(RealEstateDBContext context)
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
            var buildingProperty = await _context.BuildingProperties
                .Include(bp => bp.BuildingPropertiesImages)
                .FirstOrDefaultAsync(bp => bp.Id == propertyId);

            if (buildingProperty == null)
            {
                throw new NotFoundException($"The building property with the ID { propertyId } does not exist.");
            }

            return buildingProperty;
        }

        /// <summary>
        /// Retrieves all building properties.
        /// </summary>
        /// <returns>A collection of building properties.</returns>
        public async Task<IEnumerable<BuildingProperty>> GetBuildingProperties(BuildingPropertyFilter filter)
        {
            IQueryable<BuildingProperty> query = _context.BuildingProperties;
            var queryBuilder = new BuildingPropertyFilterQueryBuilder(query);

            queryBuilder.FilterByName(filter.Name)
                        .FilterByAddress(filter.Address)
                        .FilterByPriceRange(filter.MinPrice, filter.MaxPrice)
                        .FilterByYearRange(filter.MinYear, filter.MaxYear);

            return await queryBuilder
                .Query
                .Include(bp => bp.BuildingPropertiesImages)
                .ToListAsync();
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
