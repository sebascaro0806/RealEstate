using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstate.API.Controllers;
using RealEstate.Application.DTOs.Owner;
using RealEstate.Application.Interfaces;

namespace RealEstate.Tests.API.Controllers
{
    /// <summary>
    /// Unit test class for the OwnerController.
    /// </summary>
    [TestFixture]
    public class OwnerControllerTests
    {
        private Mock<IOwnerService> _ownerServiceMock;
        private OwnerController _controller;

        /// <summary>
        /// Initial setup that runs before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _ownerServiceMock = new Mock<IOwnerService>();
            _controller = new OwnerController(_ownerServiceMock.Object);
        }

        /// <summary>
        /// Test method to verify if CreateOwner action returns an OkResult.
        /// </summary>
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

        /// <summary>
        /// Test method to verify if GetOwners action returns an OkResult.
        /// </summary>
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
