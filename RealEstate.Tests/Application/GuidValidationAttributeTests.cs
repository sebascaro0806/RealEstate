using RealEstate.Application.DTOs;
using System.ComponentModel.DataAnnotations;


namespace RealEstate.Tests.Application
{
    [TestFixture]
    public class GuidValidationAttributeTests
    {
        private GuidValidationAttribute _guidValidationAttribute;

        [SetUp]
        public void Setup()
        {
            _guidValidationAttribute = new GuidValidationAttribute();
        }

        [Test]
        public void IsValid_ValidGuidString_ReturnsSuccess()
        {
            // Arrange
            string guidString = Guid.NewGuid().ToString();

            // Act
            var result = _guidValidationAttribute.IsValid(guidString);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsValid_InvalidGuidString_ReturnsValidationResultWithError()
        {
            // Arrange
            string invalidGuidString = "InvalidGuid";

            // Act
            var result = _guidValidationAttribute.IsValid(invalidGuidString);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
