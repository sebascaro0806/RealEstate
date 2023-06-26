using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repository
{
    public class BuildingPropertyRepository : IBuildingPropertyRepository
    {
        private readonly RealEstateDBConext _context;

        public BuildingPropertyRepository(RealEstateDBConext context)
        {
            _context = context;
        }

        public IEnumerable<BuildingProperty> GetBuildingProperties()
        {
            return _context.BuildingProperties;
        }
    }
}
