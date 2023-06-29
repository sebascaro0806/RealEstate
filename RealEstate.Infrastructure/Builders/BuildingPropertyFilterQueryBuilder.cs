using RealEstate.Domain.Models;

namespace RealEstate.Infrastructure.Builders
{
    public  class BuildingPropertyFilterQueryBuilder : BaseQueryBuilder<BuildingProperty>
    {
        public BuildingPropertyFilterQueryBuilder(IQueryable<BuildingProperty> query) : base(query)
        {
        }

        public BuildingPropertyFilterQueryBuilder FilterByName(string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Query = Query.Where(p => p.Name.Contains(name));
            }

            return this;
        }

        public BuildingPropertyFilterQueryBuilder FilterByAddress(string? address)
        {
            if (!string.IsNullOrEmpty(address))
            {
                Query = Query.Where(p => p.Address.Contains(address));
            }

            return this;
        }

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
