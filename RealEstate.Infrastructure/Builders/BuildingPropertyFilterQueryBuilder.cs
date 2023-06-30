using RealEstate.Domain.Models;

namespace RealEstate.Infrastructure.Builders
{
    /// <summary>
    /// Represents a query builder for filtering building properties based on specific criteria.
    /// Inherits from the <see cref="BaseQueryBuilder{TEntity}"/> class.
    /// </summary>
    public class BuildingPropertyFilterQueryBuilder : BaseQueryBuilder<BuildingProperty>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildingPropertyFilterQueryBuilder"/> class with the specified query.
        /// </summary>
        /// <param name="query">The queryable object representing the building properties.</param>
        public BuildingPropertyFilterQueryBuilder(IQueryable<BuildingProperty> query) : base(query)
        {
        }

        /// <summary>
        /// Filters the building properties by name.
        /// </summary>
        /// <param name="name">The name to filter by.</param>
        /// <returns>The updated query builder.</returns>
        public BuildingPropertyFilterQueryBuilder FilterByName(string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Query = Query.Where(p => p.Name.Contains(name));
            }

            return this;
        }

        /// <summary>
        /// Filters the building properties by address.
        /// </summary>
        /// <param name="address">The address to filter by.</param>
        /// <returns>The updated query builder.</returns>
        public BuildingPropertyFilterQueryBuilder FilterByAddress(string? address)
        {
            if (!string.IsNullOrEmpty(address))
            {
                Query = Query.Where(p => p.Address.Contains(address));
            }

            return this;
        }

        /// <summary>
        /// Filters the building properties by price range.
        /// </summary>
        /// <param name="minPrice">The minimum price to filter by.</param>
        /// <param name="maxPrice">The maximum price to filter by.</param>
        /// <returns>The updated query builder.</returns>
        public BuildingPropertyFilterQueryBuilder FilterByPriceRange(double? minPrice, double? maxPrice)
        {
            if (minPrice.HasValue)
            {
                Query = Query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                Query = Query.Where(p => p.Price <= maxPrice.Value);
            }

            return this;
        }

        /// <summary>
        /// Filters the building properties by year range.
        /// </summary>
        /// <param name="minYear">The minimum year to filter by.</param>
        /// <param name="maxYear">The maximum year to filter by.</param>
        /// <returns>The updated query builder.</returns>
        public BuildingPropertyFilterQueryBuilder FilterByYearRange(int? minYear, int? maxYear)
        {
            if (minYear.HasValue)
            {
                Query = Query.Where(p => p.Year >= minYear.Value);
            }

            if (maxYear.HasValue)
            {
                Query = Query.Where(p => p.Year <= maxYear.Value);
            }

            return this;
        }

        /// <summary>
        /// Filters the building properties by internal code.
        /// </summary>
        /// <param name="code">The internal code to filter by.</param>
        /// <returns>The updated query builder.</returns>
        public BuildingPropertyFilterQueryBuilder FilterByCodeInternal(int? code)
        {
            if (code.HasValue)
            {
                Query = Query.Where(p => p.CodeInternal == code);
            }

            return this;
        }
    }
}
