using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Infrastructure.Context
{
    public class RealEstateDBContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RealEstateDBConext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public RealEstateDBContext(DbContextOptions<RealEstateDBContext> options) : base(options)
        { }

        /// <summary>
        /// Gets or sets the DbSet for the BuildingProperty entity.
        /// </summary>
        public DbSet<BuildingProperty> BuildingProperties { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the BuildingPropertyImage entity.
        /// </summary>
        public DbSet<BuildingPropertyImage> BuildingPropertiesImages { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the BuildingPropertyTrace entity.
        /// </summary>
        public DbSet<BuildingPropertyTrace> BuildingPropertiesTraces { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Owner entity.
        /// </summary>
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
