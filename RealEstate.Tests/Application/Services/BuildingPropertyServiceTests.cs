using AutoMapper;
using Moq;
using RealEstate.Application.DTOs.BuildingProperty;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Interfaces;
using RealEstate.Domain.Models;
using RealEstate.Infrastructure.ExternalServices.Storage;

namespace RealEstate.Tests.Application.Services
{
    /// <summary>
    /// Represents a set of unit tests for the BuildingPropertyService class.
    /// </summary>
    [TestFixture]
    public  class BuildingPropertyServiceTests
    {

        private Mock<IBuildingPropertyRepository> _repositoryMock;
        private Mock<IStorageService> _storageBlobMock;
        private Mock<IMapper> _mapperMock;
        private BuildingPropertyService _service;

        /// <summary>
        /// Sets up the test fixture by creating the necessary mocks and initializing the BuildingPropertyService instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IBuildingPropertyRepository>();
            _storageBlobMock = new Mock<IStorageService>();
            _mapperMock = new Mock<IMapper>();
            _service = new BuildingPropertyService(_repositoryMock.Object, _storageBlobMock.Object, _mapperMock.Object);
        }

        /// <summary>
        /// Tests the GetBuildingProperties method and verifies if it returns all the building properties.
        /// </summary>
        [Test]
        public async Task GetBuildingProperties_ReturnsAllBuildingProperties()
        {
            // Arrange
            var buildingProperties = new List<BuildingProperty>
            {
                new BuildingProperty { Id = Guid.NewGuid(), Name = "Property 1" },
                new BuildingProperty { Id = Guid.NewGuid(), Name = "Property 2" },
                new BuildingProperty { Id = Guid.NewGuid(), Name = "Property 3" }
            };

            var buildingPropertyDTOs = buildingProperties.Select(bp => new BuildingPropertyDTO { Id = bp.Id, Name = bp.Name });

            _repositoryMock.Setup(repo => repo.GetBuildingProperties()).ReturnsAsync(buildingProperties);
            _mapperMock.Setup(mapper => mapper.Map<List<BuildingPropertyDTO>>(buildingProperties))
                .Returns(buildingPropertyDTOs.ToList());

            // Act
            var result = await _service.GetBuildingProperties();

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count(), Is.EqualTo(buildingPropertyDTOs.Count()));
            Assert.IsTrue(result.All(bp => buildingPropertyDTOs.Any(dto => dto.Id == bp.Id && dto.Name == bp.Name)));
        }
    }
}
