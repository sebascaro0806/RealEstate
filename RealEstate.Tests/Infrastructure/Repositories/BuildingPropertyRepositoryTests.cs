using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Filters;

namespace RealEstate.Tests.Infrastructure.Repositories
{
    /// <summary>
    /// Represents a set of unit tests for the BuildingPropertyRepository class.
    /// </summary>
    [TestFixture]
    public class BuildingPropertyRepositoryTests : BaseRepositoryTest
    {
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
                var buildingProperty = new BuildingProperty { Id = buildingPropertyId, Name = "Building 1", Address = "Quindio" };
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
        public void GetBuildingPropertyById_NonExistingId_ThrowsException()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var nonExistingId = Guid.NewGuid();

                // Act & Assert
                Assert.ThrowsAsync<NotFoundException>(async () => await repository.GetBuildingPropertyById(nonExistingId));
            }
        }

        /// <summary>
        /// Tests the GetBuildingProperties method and verifies if it returns all the building properties.
        /// </summary>
        [Test]
        public async Task GetBuildingProperties_ReturnsAllBuildingPropertiesByCodeInternal()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var buildingProperties = new List<BuildingProperty>
                {
                    new BuildingProperty { CodeInternal = 1, Id = Guid.NewGuid(), Name = "Building 1",  Address = "Quindio 1" },
                    new BuildingProperty { CodeInternal = 2, Id = Guid.NewGuid(), Name = "Building 2",  Address = "Quindio 2" },
                    new BuildingProperty { CodeInternal = 3, Id = Guid.NewGuid(), Name = "Building 3",  Address = "Quindio 3" }
                };
                context.BuildingProperties.AddRange(buildingProperties);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var filters = new BuildingPropertyFilter
                {
                    CodeInternal = 1
                };

                var result = await repository.GetBuildingProperties(filters);

                // Assert
                Assert.NotNull(result);
                Assert.That(result.Count(), Is.EqualTo(1));
            }
        }

        /// <summary>
        /// Tests the GetBuildingProperties method and verifies if it returns all the building properties.
        /// </summary>
        [Test]
        public async Task GetBuildingProperties_ReturnsAllBuildingPropertiesByAddress()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var buildingProperties = new List<BuildingProperty>
                {
                    new BuildingProperty { CodeInternal = 1, Id = Guid.NewGuid(), Name = "Building 1",  Address = "Quindio 1" },
                    new BuildingProperty { CodeInternal = 2, Id = Guid.NewGuid(), Name = "Building 2",  Address = "Quindio 2" },
                    new BuildingProperty { CodeInternal = 3, Id = Guid.NewGuid(), Name = "Building 3",  Address = "Quindio 3" }
                };
                context.BuildingProperties.AddRange(buildingProperties);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var filters = new BuildingPropertyFilter
                {
                    Address = "Quindio 2"
                };

                var result = await repository.GetBuildingProperties(filters);

                // Assert
                Assert.NotNull(result);
                Assert.That(result.Count(), Is.EqualTo(1));
            }
        }


        [Test]
        public async Task GetBuildingProperties_ReturnsAllBuildingPropertiesWithImages()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var buildingProperties = new List<BuildingProperty>
                {
                    new BuildingProperty { CodeInternal = 1, Id = Guid.NewGuid(), Name = "Building 1",  Address = "Quindio 1",
                        BuildingPropertiesImages = new List<BuildingPropertyImage> 
                        {
                            new BuildingPropertyImage { Enabled = true, Url = "test" }
                        }
                    },
                };
                context.BuildingProperties.AddRange(buildingProperties);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var filters = new BuildingPropertyFilter();

                var result = await repository.GetBuildingProperties(filters);

                // Assert
                Assert.NotNull(result);
                Assert.That(result.Count(), Is.EqualTo(1));
            }
        }


        /// <summary>
        /// Tests the GetBuildingProperties method and verifies if it returns all the building properties.
        /// </summary>
        [Test]
        public async Task GetBuildingProperties_ReturnsAllBuildingPropertiesByName()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var buildingProperties = new List<BuildingProperty>
                {
                    new BuildingProperty { CodeInternal = 1, Id = Guid.NewGuid(), Name = "Building 1",  Address = "Quindio 1" },
                    new BuildingProperty { CodeInternal = 2, Id = Guid.NewGuid(), Name = "Building 2",  Address = "Quindio 2" },
                    new BuildingProperty { CodeInternal = 3, Id = Guid.NewGuid(), Name = "Building 3",  Address = "Quindio 3" }
                };
                context.BuildingProperties.AddRange(buildingProperties);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var filters = new BuildingPropertyFilter
                {
                    Name = "Building 1"
                };

                var result = await repository.GetBuildingProperties(filters);

                // Assert
                Assert.NotNull(result);
                Assert.That(result.Count(), Is.EqualTo(1));
            }
        }

        /// <summary>
        /// Tests the GetBuildingProperties method and verifies if it returns all the building properties.
        /// </summary>
        [Test]
        public async Task GetBuildingProperties_ReturnsAllBuildingPropertiesByMinPriceAndMaxPrice()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var buildingProperties = new List<BuildingProperty>
                {
                    new BuildingProperty { CodeInternal = 1, Id = Guid.NewGuid(), Name = "Building 1",  Address = "Quindio 1", Price = 151111 },
                    new BuildingProperty { CodeInternal = 2, Id = Guid.NewGuid(), Name = "Building 2",  Address = "Quindio 2", Price = 101111 },
                    new BuildingProperty { CodeInternal = 3, Id = Guid.NewGuid(), Name = "Building 3",  Address = "Quindio 3", Price = 251111 }
                };
                context.BuildingProperties.AddRange(buildingProperties);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var filters = new BuildingPropertyFilter
                {
                    MinPrice = 101111,
                    MaxPrice = 151111
                };

                var result = await repository.GetBuildingProperties(filters);

                // Assert
                Assert.NotNull(result);
                Assert.That(result.Count(), Is.EqualTo(2));
            }
        }

        /// <summary>
        /// Tests the GetBuildingProperties method and verifies if it returns all the building properties.
        /// </summary>
        [Test]
        public async Task GetBuildingProperties_ReturnsAllBuildingPropertiesByMinYearAndMaxYear()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var buildingProperties = new List<BuildingProperty>
                {
                    new BuildingProperty { CodeInternal = 1, Id = Guid.NewGuid(), Name = "Building 1",  Address = "Quindio 1", Year = 2012 },
                    new BuildingProperty { CodeInternal = 2, Id = Guid.NewGuid(), Name = "Building 2",  Address = "Quindio 2", Year = 2012 },
                    new BuildingProperty { CodeInternal = 3, Id = Guid.NewGuid(), Name = "Building 3",  Address = "Quindio 3", Year = 2015 }
                };
                context.BuildingProperties.AddRange(buildingProperties);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var filters = new BuildingPropertyFilter
                {
                    MinYear = 2012,
                    MaxYear = 2015
                };

                var result = await repository.GetBuildingProperties(filters);

                // Assert
                Assert.NotNull(result);
                Assert.That(result.Count(), Is.EqualTo(3));
            }
        }

        /// <summary>
        /// Tests the GetBuildingProperties method and verifies if it returns all the building properties.
        /// </summary>
        [Test]
        public async Task GetBuildingProperties_ReturnsAllBuildingPropertiesWithPagination()
        {
            // Arrange
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var buildingProperties = new List<BuildingProperty>
                {
                    new BuildingProperty { CodeInternal = 1, Id = Guid.NewGuid(), Name = "Building 1",  Address = "Quindio 1", Year = 2012 },
                    new BuildingProperty { CodeInternal = 2, Id = Guid.NewGuid(), Name = "Building 2",  Address = "Quindio 2", Year = 2012 },
                    new BuildingProperty { CodeInternal = 3, Id = Guid.NewGuid(), Name = "Building 3",  Address = "Quindio 3", Year = 2015 }
                };
                context.BuildingProperties.AddRange(buildingProperties);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new RealEstateDBContext(_dbContextOptions))
            {
                var repository = new BuildingPropertyRepository(context);
                var filters = new BuildingPropertyFilter
                {
                    PageNumber = 2,
                    PageSize = 1
                };

                var result = await repository.GetBuildingProperties(filters);

                // Assert
                Assert.NotNull(result);
                Assert.That(result.Count(), Is.EqualTo(1));
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
