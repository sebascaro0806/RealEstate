using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstate.API.Controllers;
using RealEstate.Application.DTOs.BuildingProperty;
using RealEstate.Application.Interfaces;
using System.IO;
using System.Text;

namespace RealEstate.Tests.API.Controllers
{
    [TestFixture]
    public class BuildingPropertyControllerTests
    {
        private Mock<IBuildingPropertyService> _buildingPropertyServiceMock;
        private BuildingPropertyController _controller;

        [SetUp]
        public void Setup()
        {
            _buildingPropertyServiceMock = new Mock<IBuildingPropertyService>();
            _controller = new BuildingPropertyController(_buildingPropertyServiceMock.Object);
        }

        [Test]
        public async Task GetProperties_ReturnsOkResult()
        {
            // Arrange
            var filter = new BuildingPropertyFilterDTO();

            // Act
            var result = await _controller.GetProperties(filter);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task CreateProperty_ReturnsCreatedResult()
        {
            // Arrange
            var propertyDTO = new CreateBuildingPropertyDTO();

            // Act
            _buildingPropertyServiceMock.Setup(c => c.CreateBuildingProperty(propertyDTO)).ReturnsAsync(new BuildingPropertyDTO());
            var result = await _controller.CreateProperty(propertyDTO);

            // Assert
            Assert.IsInstanceOf<CreatedResult>(result);
        }

        [Test]
        public async Task AddPropertyImage_ReturnsCreatedResult()
        {
            // Arrange
            var propertyId = "123";
            var fileStream = new MemoryStream(Encoding.UTF8.GetBytes("Test file content"));
            var formFileMock = new Mock<IFormFile>();
            var stream = new MemoryStream();
            formFileMock.Setup(f => f.OpenReadStream()).Returns(stream);

            // Act
            _buildingPropertyServiceMock.Setup(c => c.AddImageToBuildingProperty(propertyId, "test", fileStream));
            var result = await _controller.AddPropertyImage(propertyId, formFileMock.Object);

            // Assert
            Assert.IsInstanceOf<CreatedResult>(result);
        }

        [Test]
        public async Task ChangePropertyPrice_ReturnsNoContentResult()
        {
            // Arrange
            var propertyId = "123";
            var price = 100.0;

            // Act
            _buildingPropertyServiceMock.Setup(c => c.ChangeBuildingPropertyPrice(propertyId, 15));
            var result = await _controller.ChangePropertyPrice(propertyId, price);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task UpdateBuildingProperty_ReturnsNoContentResult()
        {
            // Arrange
            var propertyId = "123";
            var propertyDTO = new UpdateBuildingPropertyDTO();

            // Act
            _buildingPropertyServiceMock.Setup(c => c.UpdateBuildingProperty(propertyId, propertyDTO));
            var result = await _controller.UpdateBuildingProperty(propertyId, propertyDTO);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}
