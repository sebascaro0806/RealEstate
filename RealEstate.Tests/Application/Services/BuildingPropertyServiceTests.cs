using AutoMapper;
using Moq;
using RealEstate.Application.DTOs.BuildingProperty;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Filters;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.ExternalServices.Storage;
using System.Text;

namespace RealEstate.Tests.Application.Services
{
    /// <summary>
    /// Represents a set of unit tests for the BuildingPropertyService class.
    /// </summary>
    [TestFixture]
    public  class BuildingPropertyServiceTests
    {

        private Mock<IOwnerRepository> _ownerRepositoryMock;
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
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _storageBlobMock = new Mock<IStorageService>();
            _mapperMock = new Mock<IMapper>();

            _service = new BuildingPropertyService(_repositoryMock.Object,
                _ownerRepositoryMock.Object, 
                _storageBlobMock.Object, 
                _mapperMock.Object);
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

            var buildingPropertyDTOs = buildingProperties
                .Select(bp => new BuildingPropertyDTO 
                { 
                    Id = bp.Id,
                    Name = bp.Name, 
                    BuildingPropertiesImages = new List<BuildingPropertyImageDTO>() 
                    { 
                        new BuildingPropertyImageDTO()
                    } 
                });

            var filter = new BuildingPropertyFilterDTO();

            _repositoryMock.Setup(repo => repo.GetBuildingProperties(It.IsAny<BuildingPropertyFilter>())).ReturnsAsync(buildingProperties);

            _mapperMock.Setup(mapper => mapper.Map<BuildingPropertyFilter>(filter))
                .Returns(It.IsAny<BuildingPropertyFilter>());

            _mapperMock.Setup(mapper => mapper.Map<List<BuildingPropertyDTO>>(buildingProperties))
                .Returns(buildingPropertyDTOs.ToList());

            // Act
            var result = await _service.GetBuildingProperties(filter);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count(), Is.EqualTo(buildingPropertyDTOs.Count()));
            Assert.IsTrue(result.All(bp => buildingPropertyDTOs.Any(dto => dto.Id == bp.Id && dto.Name == bp.Name)));
        }

        /// <summary>
        /// Test for the CreateBuildingProperty function in the service.
        /// </summary>
        [Test]
        public async Task CreateBuildingProperty_ValidInput_ReturnsBuildingPropertyDTO()
        {
            // Arrange
            var buildingPropertyDTO = new CreateBuildingPropertyDTO
            {
                OwnerId = "b2858ed6-616a-4b95-bb5f-6e590db2c8cc"
            };

            _mapperMock.Setup(mapper => mapper.Map<BuildingProperty>(buildingPropertyDTO))
               .Returns(new BuildingProperty());

            _mapperMock.Setup(mapper => mapper.Map<BuildingPropertyDTO>(It.IsAny<BuildingProperty>()))
               .Returns(new BuildingPropertyDTO());

            _ownerRepositoryMock.Setup(x => x.GetOwnerById(Guid.Parse(buildingPropertyDTO.OwnerId)))
                .ReturnsAsync(new Owner
                {
                    Id = Guid.Parse(buildingPropertyDTO.OwnerId),
                    Name = "sebastian"
                });

            _repositoryMock.Setup(x => x.CreateBuildingProperty(It.IsAny<BuildingProperty>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateBuildingProperty(buildingPropertyDTO);

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test for the AddImageToBuildingProperty function when the property exists.
        /// It should add an image to the property.
        /// </summary>
        [Test]
        public async Task AddImageToBuildingProperty_WhenPropertyExists_ShouldAddImageToProperty()
        {
            // Arrange
            Guid propertyId = Guid.NewGuid();
            string filename = "test-image.jpg";
            var fileStream = new MemoryStream(Encoding.UTF8.GetBytes("Test file content"));

            var buildingProperty = new BuildingProperty
            {
                Id = propertyId,
                BuildingPropertiesImages = new List<BuildingPropertyImage>()
            };

            _storageBlobMock.Setup(service => service.UploadFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()))
                             .ReturnsAsync("image-url");

            _repositoryMock.Setup(repo => repo.GetBuildingPropertyById(propertyId))
                                         .ReturnsAsync(buildingProperty);

            // Act
            await _service.AddImageToBuildingProperty(propertyId.ToString(), filename, fileStream);

            // Assert
            Assert.That(buildingProperty.BuildingPropertiesImages.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Test for the AddImageToBuildingProperty function when the property exists and the images property is null.
        /// It should add an image to the property.
        /// </summary>
        [Test]
        public async Task AddImageToBuildingProperty_WhenPropertyExists_AndNullImagesProperty_ShouldAddImageToProperty()
        {
            // Arrange
            Guid propertyId = Guid.NewGuid();
            string filename = "test-image.jpg";
            var fileStream = new MemoryStream(Encoding.UTF8.GetBytes("Test file content"));

            var buildingProperty = new BuildingProperty
            {
                Id = propertyId
            };

            _storageBlobMock.Setup(service => service.UploadFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()))
                             .ReturnsAsync("image-url");

            _repositoryMock.Setup(repo => repo.GetBuildingPropertyById(propertyId))
                                         .ReturnsAsync(buildingProperty);

            // Act
            await _service.AddImageToBuildingProperty(propertyId.ToString(), filename, fileStream);

            // Assert
            Assert.That(buildingProperty.BuildingPropertiesImages.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Test for the ChangeBuildingPropertyPrice function when the property exists.
        /// It should update the price of the property.
        /// </summary>
        [Test]
        public async Task ChangeBuildingPropertyPrice_WhenPropertyExists_ShouldUpdatePrice()
        {
            // Arrange
            Guid propertyId = Guid.NewGuid();
            double newPrice = 150000;

            var buildingProperty = new BuildingProperty
            {
                Id = propertyId,
                Price = 100000
            };

            _repositoryMock.Setup(repo => repo.GetBuildingPropertyById(propertyId))
                                         .ReturnsAsync(buildingProperty);
            // Act
            await _service.ChangeBuildingPropertyPrice(propertyId.ToString(), newPrice);

            // Assert
            Assert.That(buildingProperty.Price, Is.EqualTo(newPrice));
        }

        /// <summary>
        /// Test for the UpdateBuildingProperty function when the property exists.
        /// It should update the details of the property.
        /// </summary>
        [Test]
        public async Task UpdateBuildingProperty_WhenPropertyExists_ShouldUpdatePropertyDetails()
        {
            // Arrange
            Guid propertyId = Guid.NewGuid();
            var buildingPropertyDTO = new UpdateBuildingPropertyDTO
            {
                Name = "Updated Property",
                Address = "Updated Address",
                Price = 150000,
                Year = 2022
            };

            var buildingProperty = new BuildingProperty
            {
                Id = propertyId,
                Name = "Old Property",
                Address = "Old Address",
                Price = 100000,
                Year = 2020
            };

            _repositoryMock.Setup(repo => repo.GetBuildingPropertyById(propertyId))
                                         .ReturnsAsync(buildingProperty);

            // Act
            await _service.UpdateBuildingProperty(propertyId.ToString(), buildingPropertyDTO);

            // Assert
            Assert.That(buildingProperty.Name, Is.EqualTo(buildingPropertyDTO.Name));
            Assert.That(buildingProperty.Address, Is.EqualTo(buildingPropertyDTO.Address));
            Assert.That(buildingProperty.Price, Is.EqualTo(buildingPropertyDTO.Price));
            Assert.That(buildingProperty.Year, Is.EqualTo(buildingPropertyDTO.Year));
        }
    }
}
