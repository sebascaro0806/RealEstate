using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Models;

namespace RealEstate.Infrastructure.Builders
{
    public  class BuildingPropertyFilterQueryBuilder
    {
        private IQueryable<BuildingProperty> _query;

        public BuildingPropertyFilterQueryBuilder(IQueryable<BuildingProperty> query)
        {
            _query = query;
        }

        public IQueryable<BuildingProperty> Query => _query;

        public BuildingPropertyFilterQueryBuilder FilterByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _query = _query.Where(p => p.Name.Contains(name));
            }

            return this;
        }

        public BuildingPropertyFilterQueryBuilder FilterByAddress(string address)
        {
            if (!string.IsNullOrEmpty(address))
            {
                _query = _query.Where(p => p.Address.Contains(address));
            }

            return this;
        }

        public BuildingPropertyFilterQueryBuilder FilterByPriceRange(double? minPrice, double? maxPrice)
        {
            if (minPrice.HasValue)
            {
                _query = _query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                _query = _query.Where(p => p.Price <= maxPrice.Value);
            }

            return this;
        }

        public BuildingPropertyFilterQueryBuilder FilterByYearRange(int? minYear, int? maxYear)
        {
            if (minYear.HasValue)
            {
                _query = _query.Where(p => p.Year >= minYear.Value);
            }

            if (maxYear.HasValue)
            {
                _query = _query.Where(p => p.Year <= maxYear.Value);
            }

            return this;
        }
    }
}
