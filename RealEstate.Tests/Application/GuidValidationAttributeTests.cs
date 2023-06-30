using RealEstate.Application.DTOs;

namespace RealEstate.Tests.Application
{
    /// <summary>
    /// Unit tests for the <see cref="GuidValidationAttribute"/> class.
    /// </summary>
    [TestFixture]
    public class GuidValidationAttributeTests
    {
        private GuidValidationAttribute _guidValidationAttribute;

        /// <summary>
        /// Initial setup that runs before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _guidValidationAttribute = new GuidValidationAttribute();
        }

        /// <summary>
        /// Tests the <see cref="GuidValidationAttribute.IsValid"/> method with a valid GUID string.
        /// It should return true.
        /// </summary>
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

        /// <summary>
        /// Tests the <see cref="GuidValidationAttribute.IsValid"/> method with an invalid GUID string.
        /// It should return false.
        /// </summary>
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
