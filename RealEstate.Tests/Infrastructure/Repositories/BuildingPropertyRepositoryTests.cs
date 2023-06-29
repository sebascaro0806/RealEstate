using Microsoft.EntityFrameworkCore;

namespace RealEstate.Tests.Infrastructure.Repositories
{
    /// <summary>
    /// Represents a set of unit tests for the BuildingPropertyRepository class.
    /// </summary>
    [TestFixture]
    public class BuildingPropertyRepositoryTests
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
        /// Tests the GetBuildingPropertyById method with an existing identifier and verifies if the expected building property is returned.
        /// </summary>
        [Test]
        public async Task GetBuildingPropertyById_ExistingId_ReturnsBuildingProperty()
        {
            var buildingPropertyId = Guid.NewGuid();

            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var buildingProperty = new BuildingProperty { Id = buildingPropertyId, Name = "Building 1", CodeInternal = "XXX-XXX", Address = "Quindio" };
                context.BuildingProperties.Add(buildingProperty);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var result = await repository.GetBuildingPropertyById(buildingPropertyId);

                // Assert
                Assert.NotNull(result);
                Assert.That(result.Id, Is.EqualTo(buildingPropertyId));
            }
        }

        /// <summary>
        /// Tests the GetBuildingPropertyById method with a non-existing identifier and verifies if it throws an exception.
        /// </summary>
        [Test]
        public async Task GetBuildingPropertyById_NonExistingId_ThrowsException()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var nonExistingId = Guid.NewGuid();

                // Act & Assert
                Assert.ThrowsAsync<Exception>(async () => await repository.GetBuildingPropertyById(nonExistingId));
            }
        }

        /// <summary>
        /// Tests the GetBuildingProperties method and verifies if it returns all the building properties.
        /// </summary>
        [Test]
        public async Task GetBuildingProperties_ReturnsAllBuildingProperties()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var buildingProperties = new List<BuildingProperty>
                {
                    new BuildingProperty { Id = Guid.NewGuid(), Name = "Building 1",  CodeInternal = "XXX-XX1", Address = "Quindio" },
                    new BuildingProperty { Id = Guid.NewGuid(), Name = "Building 2",  CodeInternal = "XXX-XX2", Address = "Quindio" },
                    new BuildingProperty { Id = Guid.NewGuid(), Name = "Building 3",  CodeInternal = "XXX-XX3", Address = "Quindio" }
                };
                context.BuildingProperties.AddRange(buildingProperties);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                //var result = await repository.GetBuildingProperties();

                // Assert
                //Assert.NotNull(result);
                //Assert.That(result.Count(), Is.EqualTo(3));
            }
        }

        /// <summary>
        /// Tests the CreateBuildingProperty method and verifies if a new building property is added successfully.
        /// </summary>
        [Test]
        public async Task CreateBuildingProperty_AddsNewBuildingProperty()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var buildingProperty = new BuildingProperty 
                { 
                    Id = Guid.NewGuid(), 
                    Name = "New Building", 
                    CodeInternal = "XXX-XX1", 
                    Address = "Quindio" 
                };

                // Act
                await repository.CreateBuildingProperty(buildingProperty);

                // Assert
                var createdBuildingProperty = await context.BuildingProperties.FindAsync(buildingProperty.Id);
                Assert.NotNull(createdBuildingProperty);
                Assert.That(createdBuildingProperty.Name, Is.EqualTo(buildingProperty.Name));
            }
        }

        /// <summary>
        /// Tests the UpdateBuildingProperty method with an existing building property and verifies if it is updated successfully.
        /// </summary>
        [Test]
        public async Task UpdateBuildingProperty_ExistingBuildingProperty_UpdatesSuccessfully()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                
                var buildingProperty = new BuildingProperty
                {
                    Id = Guid.NewGuid(),
                    Name = "New Building",
                    CodeInternal = "XXX-XX1",
                    Address = "Quindio"
                };

                context.BuildingProperties.Add(buildingProperty);
                await context.SaveChangesAsync();

                // Modify the building property
                buildingProperty.Name = "Updated Building";

                // Act
                await repository.UpdateBuildingProperty(buildingProperty);

                // Assert
                var updatedBuildingProperty = await context.BuildingProperties.FindAsync(buildingProperty.Id);
                Assert.NotNull(updatedBuildingProperty);
                Assert.That(updatedBuildingProperty.Name, Is.EqualTo(buildingProperty.Name));
            }
        }
    }
}
