using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using RealEstate.API.Filters;

namespace RealEstate.Tests.API.Filters
{
    /// <summary>
    /// Unit tests for the <see cref="ValidateGuidIdAttribute"/> class.
    /// </summary>
    [TestFixture]
    public class ValidateGuidIdAttributeTests
    {
        private ValidateGuidIdAttribute _attribute;
        private ActionExecutingContext _context;

        /// <summary>
        /// Initial setup that runs before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _attribute = new ValidateGuidIdAttribute("propertyId");
            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor()
            );
            _context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new object()
            );
        }

        /// <summary>
        /// Tests the <see cref="ValidateGuidIdAttribute.OnActionExecutionAsync"/> method when a valid GUID is provided.
        /// It should return the next delegate without setting a result.
        /// </summary>
        [Test]
        public async Task OnActionExecutionAsync_WithValidGuidId_ReturnsNextDelegate()
        {
            // Arrange
            var validGuid = Guid.NewGuid();
            _context.ActionArguments["propertyId"] = validGuid.ToString();

            // Act
            await _attribute.OnActionExecutionAsync(_context, () => Task.FromResult(new ActionExecutedContext(_context, new List<IFilterMetadata>(), null)));

            // Assert
            Assert.IsNull(_context.Result);
        }

        /// <summary>
        /// Tests the <see cref="ValidateGuidIdAttribute.OnActionExecutionAsync"/> method when an invalid GUID is provided.
        /// It should return a BadRequestObjectResult.
        /// </summary>
        [Test]
        public async Task OnActionExecutionAsync_WithInvalidGuidId_ReturnsBadRequest()
        {
            // Arrange
            var invalidGuid = "invalid-guid";
            _context.ActionArguments["propertyId"] = invalidGuid;

            // Act
            await _attribute.OnActionExecutionAsync(_context, () => Task.FromResult(new ActionExecutedContext(_context, new List<IFilterMetadata>(), null)));

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(_context.Result);
        }
    }
}
