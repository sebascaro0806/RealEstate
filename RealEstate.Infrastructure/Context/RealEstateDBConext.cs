using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Models;

namespace RealEstate.Infrastructure.Context
{
    public class RealEstateDBConext : DbContext
    {
        public RealEstateDBConext(DbContextOptions<RealEstateDBConext> options) : base(options)
        { }

        public DbSet<BuildingProperty> BuildingProperties { get; set; }
    }
}
