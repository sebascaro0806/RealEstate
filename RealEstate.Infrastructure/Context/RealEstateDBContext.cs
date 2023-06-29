using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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

        /// <summary>
        /// Method used to configure the relationships between entities in the database model.
        /// It overrides the OnModelCreating method of the base DbContext class.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder object used to configure the entities and relationships.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>()
                .HasMany(o => o.BuildingProperties)
                .WithOne(bp => bp.Owner)
                .HasForeignKey(bp => bp.OwnerId);

            modelBuilder.Entity<BuildingProperty>()
                .HasMany(bpi => bpi.BuildingPropertiesImages)
                .WithOne(bp => bp.BuildingProperty)
                .HasForeignKey(bp => bp.BuildingPropertyId);

            modelBuilder.Entity<BuildingProperty>()
                .Property(p => p.CodeInternal)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}
