using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Infrastructure.Context
{
    public class RealEstateDBConext : DbContext
    {
        public RealEstateDBConext(DbContextOptions<RealEstateDBConext> options) : base(options)
        { }

        public DbSet<BuildingProperty> BuildingProperties { get; set; }
        public DbSet<BuildingPropertyImage> BuildingPropertiesImages { get; set; }
        public DbSet<BuildingPropertyTrace> BuildingPropertiesTraces { get; set; }
        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>()
                .HasMany(o => o.BuildingProperties)
                .WithOne(bp => bp.Owner)
                .HasForeignKey(bp => bp.OwnerId);

            modelBuilder.Entity<BuildingPropertyImage>()
                .HasOne(bpi => bpi.BuildingProperty)
                .WithOne(bp => bp.BuildingPropertyImage)
                .HasForeignKey<BuildingPropertyImage>(bpi => bpi.BuildingPropertyId);
        }
    }
}
