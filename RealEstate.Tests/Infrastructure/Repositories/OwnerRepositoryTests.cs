using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Models;
using RealEstate.Infrastructure.Context;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Tests.Infrastructure.Repositories
{
    /// <summary>
    /// Represents a set of unit tests for the OwnerRepository class.
    /// </summary>
    [TestFixture]
    public class OwnerRepositoryTests
    {

        /// <summary>
        /// The options for configuring the in-memory database context.
        /// </summary>
        private DbContextOptions<RealEstateDBContext> _dbContextOptions;

        /// <summary>
        /// Sets up the test fixture by configuring the in-memory database options.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<RealEstateDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        /// <summary>
        /// Tears down the test fixture by deleting the in-memory database.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
            }
        }

        /// <summary>
        /// Tests the GetOwners method of the OwnerRepository class to ensure it returns all owners.
        /// </summary>
        [Test]
        public async Task GetOwners_ReturnsAllOwners()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new OwnerRepository(context);

                var owner1 = new Owner { Id = Guid.NewGuid(), Name = "Owner 1", Address = "Quindio" };
                var owner2 = new Owner { Id = Guid.NewGuid(), Name = "Owner 2", Address = "Quindio" };

                context.Owners.Add(owner1);
                context.Owners.Add(owner2);
                await context.SaveChangesAsync();

                // Act
                var owners = await repository.GetOwners();

                // Assert
                Assert.NotNull(owners);
                Assert.That(owners.Count(), Is.EqualTo(2));
                Assert.IsTrue(owners.Any(o => o.Id == owner1.Id && o.Name == owner1.Name));
                Assert.IsTrue(owners.Any(o => o.Id == owner2.Id && o.Name == owner2.Name));
            }
        }

        /// <summary>
        /// Tests the CreateOwner method of the OwnerRepository class to ensure it adds a new owner.
        /// </summary>
        [Test]
        public async Task CreateOwner_AddsNewOwner()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new OwnerRepository(context);
                var owner = new Owner { Id = Guid.NewGuid(), Name = "New Owner", Address = "Quindio" };

                // Act
                await repository.CreateOwner(owner);

                // Assert
                var createdOwner = await context.Owners.FindAsync(owner.Id);
                Assert.NotNull(createdOwner);
                Assert.That(createdOwner.Name, Is.EqualTo(owner.Name));
            }
        }
    }
}
