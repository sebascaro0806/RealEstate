namespace RealEstate.Infrastructure.ExternalServices.Storage
{
    /// <summary>
    /// Represents a service for interacting with an external storage.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Uploads a file to the specified container in the storage service.
        /// </summary>
        /// <param name="containerName">The name of the container.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="fileStream">The stream containing the file data.</param>
        /// <returns>The URL of the uploaded file.</returns>
        Task<string> UploadFileAsync(string containerName, string fileName, Stream fileStream);
    }
}
