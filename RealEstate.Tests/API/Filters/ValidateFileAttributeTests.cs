using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using RealEstate.API.Filters;


namespace RealEstate.Tests.API.Filters
{
    /// <summary>
    /// Unit test class for the ValidateFileAttribute.
    /// </summary>
    [TestFixture]
    public class ValidateFileAttributeTests
    {
        private ValidateFileAttribute _attribute;
        private ActionExecutingContext _context;

        /// <summary>
        /// Initial setup that runs before each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _attribute = new ValidateFileAttribute(10485760, ".jpg", ".png");
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
        /// Test method to verify if OnActionExecutionAsync returns the next delegate when provided with a valid file.
        /// </summary>
        [Test]
        public async Task OnActionExecutionAsync_WithValidFile_ReturnsNextDelegate()
        {
            // Arrange
            var fileStream = new MemoryStream(new byte[] { 0x00, 0x01, 0x02 });
            var file = new FormFile(fileStream, 0, fileStream.Length, "file", "file.jpg");
            _context.HttpContext.Request.Form = new FormCollection(
                new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>(), new FormFileCollection { file });

            // Act
            await _attribute.OnActionExecutionAsync(_context, 
                () => Task.FromResult(new ActionExecutedContext(_context, new List<IFilterMetadata>(), null)));

            // Assert
            Assert.IsNull(_context.Result);
        }

        /// <summary>
        /// Test method to verify if OnActionExecutionAsync returns BadRequestObjectResult when provided with a file with an invalid extension.
        /// </summary>
        [Test]
        public async Task OnActionExecutionAsync_WithInvalidFileExtension_ReturnsBadRequest()
        {
            // Arrange
            var fileStream = new MemoryStream(new byte[] { 0x00, 0x01, 0x02 });
            var file = new FormFile(fileStream, 0, fileStream.Length, "file", "file.txt");
            _context.HttpContext.Request.Form = new FormCollection(
                new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>(), new FormFileCollection { file });

            // Act
            await _attribute.OnActionExecutionAsync(_context, 
                () => Task.FromResult(new ActionExecutedContext(_context, new List<IFilterMetadata>(), null)));

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(_context.Result);
        }

        /// <summary>
        /// Test method to verify if OnActionExecutionAsync returns BadRequestObjectResult when provided with a file exceeding the maximum file size.
        /// </summary>
        [Test]
        public async Task OnActionExecutionAsync_WithInvalidFileSize_ReturnsBadRequest()
        {
            // Arrange
            var fileStream = new MemoryStream(new byte[] { 0x00, 0x01, 0x02 });
            var file = new FormFile(fileStream, 0, fileStream.Length, "file", "file.jpg");
            _context.HttpContext.Request.Form = new FormCollection(
                new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>(), new FormFileCollection { file });

            _attribute = new ValidateFileAttribute(2, ".jpg", ".png"); // Set maximum file size to 2 bytes

            // Act
            await _attribute.OnActionExecutionAsync(_context, 
                () => Task.FromResult(new ActionExecutedContext(_context, new List<IFilterMetadata>(), null)));

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(_context.Result);
        }

        /// <summary>
        /// Test method to verify if OnActionExecutionAsync returns BadRequestObjectResult when no file is found.
        /// </summary>
        [Test]
        public async Task OnActionExecutionAsync_WithNotFoundFile_ReturnsBadRequest()
        {
            // Act
            await _attribute.OnActionExecutionAsync(_context,
                () => Task.FromResult(new ActionExecutedContext(_context, new List<IFilterMetadata>(), null)));

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(_context.Result);
        }

        /// <summary>
        /// Test method to verify if OnActionExecutionAsync returns BadRequestObjectResult when a null file is provided.
        /// </summary>
        [Test]
        public async Task OnActionExecutionAsync_WithNullFile_ReturnsBadRequest()
        {
            _context.HttpContext.Request.Form = new FormCollection(
                new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>(), null);

            // Act
            await _attribute.OnActionExecutionAsync(_context,
                () => Task.FromResult(new ActionExecutedContext(_context, new List<IFilterMetadata>(), null)));

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(_context.Result);
        }
    }
}
