using AutoMapper;
using Moq;
using RealEstate.Application.DTOs.Owner;
using RealEstate.Application.Interfaces;
using RealEstate.Application.Services;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Tests.Application.Services
{
    [TestFixture]
    public class OwnerServiceTests
    {
        private Mock<IOwnerRepository> _repositoryMock;
        private Mock<IMapper> _mapperMock;
        private OwnerService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IOwnerRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new OwnerService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetOwners_ShouldReturnListOfOwnerDTOs()
        {
            // Arrange
            var owners = new List<OwnerDTO>
            {
                new OwnerDTO { Id = Guid.NewGuid(), Name = "Owner 3" }
            };

            _repositoryMock.Setup(repo => repo.GetOwners())
                              .ReturnsAsync(new List<Owner>());

            _mapperMock.Setup(mapper => mapper.Map<List<OwnerDTO>>(It.IsAny<List<Owner>>()))
                      .Returns(owners);

            // Act
            var result = await _service.GetOwners();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IEnumerable<OwnerDTO>>());
            Assert.That(result.Count(), Is.EqualTo(owners.Count));
        }

        [Test]
        public async Task CreateOwner_ShouldCreateNewOwnerAndReturnOwnerDTO()
        {
            // Arrange
            var ownerDTO = new CreateOwnerDTO { Name = "New Owner" };
            var owner = new Owner { Id = Guid.NewGuid(), Name = ownerDTO.Name };

            _repositoryMock.Setup(repo => repo.CreateOwner(It.IsAny<Owner>()))
                              .Returns(Task.CompletedTask)
                              .Callback((Owner createdOwner) => owner.Id = createdOwner.Id);

            _mapperMock.Setup(mapper => mapper.Map<Owner>(ownerDTO))
                      .Returns(owner);

            _mapperMock.Setup(mapper => mapper.Map<OwnerDTO>(owner))
                      .Returns(new OwnerDTO { Id = owner.Id, Name = owner.Name });


            // Act
            var result = await _service.CreateOwner(ownerDTO);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<OwnerDTO>());
            Assert.That(result.Id, Is.EqualTo(owner.Id));
            Assert.That(result.Name, Is.EqualTo(owner.Name));
        }
    }
}
