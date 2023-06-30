using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstate.API.Controllers;
using RealEstate.Application.DTOs.Owner;
using RealEstate.Application.Interfaces;

namespace RealEstate.Tests.API.Controllers
{
    [TestFixture]
    public class OwnerControllerTests
    {
        private Mock<IOwnerService> _ownerServiceMock;
        private OwnerController _controller;

        [SetUp]
        public void Setup()
        {
            _ownerServiceMock = new Mock<IOwnerService>();
            _controller = new OwnerController(_ownerServiceMock.Object);
        }

        [Test]
        public async Task CreateOwner_ReturnsOkResult()
        {
            // Arrange
            var ownerDTO = new CreateOwnerDTO();

            // Act
            var result = await _controller.CreateOwner(ownerDTO);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetOwners_ReturnsOkResult()
        {
            // Act
            var result = await _controller.GetOwners();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
