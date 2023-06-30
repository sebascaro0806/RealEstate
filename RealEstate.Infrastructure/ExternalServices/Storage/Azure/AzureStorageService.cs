using Azure.Storage.Blobs;

namespace RealEstate.Infrastructure.ExternalServices.Storage.Azure
{
    /// <summary>
    /// Implementation of the <see cref="IStorageService"/> interface using Azure Blob Storage.
    /// </summary>
    public class AzureStorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        /// <summary>
        /// Initializes a new instance of the AzureStorageService class.
        /// </summary>
        /// <param name="connectionString">The connection string for the Azure Storage account.</param>
        public AzureStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        /// <summary>
        /// Uploads a file to the specified container in Azure Blob Storage.
        /// </summary>
        /// <param name="containerName">The name of the container.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="fileStream">The stream representing the file content.</param>
        /// <returns>The URL of the uploaded file.</returns>
        public async Task<string> UploadFileAsync(string containerName, string fileName, Stream fileStream)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);
            return blobClient.Uri.ToString();
        }
    }
}
