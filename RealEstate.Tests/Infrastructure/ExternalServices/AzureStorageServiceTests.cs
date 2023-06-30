using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Moq;
using RealEstate.Infrastructure.ExternalServices.Storage;
using RealEstate.Infrastructure.ExternalServices.Storage.Azure;
using System.Text;
using System.Threading;

namespace RealEstate.Tests.ExternalServices
{
    [TestFixture]
    public class AzureStorageServiceTests
    {
        private AzureStorageService _azureStorageService;
        private Mock<BlobServiceClient> _mockBlobServiceClient;

        [SetUp]
        public void SetUp()
        {
            _mockBlobServiceClient = new Mock<BlobServiceClient>();
            _azureStorageService = new AzureStorageService(_mockBlobServiceClient.Object);
        }

        [Test]
        public async Task UploadFileAsync_UploadsFileToAzureBlobStorage()
        {
            // Arrange
            var containerName = "test-container";
            var fileName = "test-file.txt";
            var fileStream = new MemoryStream(Encoding.UTF8.GetBytes("Test file content"));

            var mockContainerClient = new Mock<BlobContainerClient>();
            var mockBlobClient = new Mock<BlobClient>();

            _mockBlobServiceClient.Setup(x => x.GetBlobContainerClient(containerName)).Returns(mockContainerClient.Object);

            mockContainerClient.Setup(x =>
            x.CreateIfNotExistsAsync(It.IsAny<PublicAccessType>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<BlobContainerEncryptionScopeOptions>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(It.IsAny<Response<BlobContainerInfo>>()));

            mockContainerClient.Setup(x => x.GetBlobClient(fileName)).Returns(mockBlobClient.Object);

            mockBlobClient.Setup(c => c.UploadAsync(It.IsAny<Stream>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Returns((Stream stream, bool overwrite, CancellationToken cancellationToken) => Task.FromResult(It.IsAny<Response<BlobContentInfo>>()));
            mockBlobClient.Setup(x => x.Uri).Returns(new Uri("https://example.com/test-file.txt"));

            // Act
            var result = await _azureStorageService.UploadFileAsync(containerName, fileName, fileStream);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(mockBlobClient.Object.Uri.ToString()));
        }
    }
}
