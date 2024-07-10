using Azure.Storage.Blobs;

namespace FileConvertor.Services
{
    /// <summary>
    /// This interface represents the Azure storage service.
    /// </summary>
    public interface IAzureStorageService
    {
        /// <summary>
        /// Uploads the file asynchronous.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        Task UploadFileAsync(string filePath);

        /// <summary>
        /// Gets the container client asynchronous.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns>The blob container client.</returns>
        Task<BlobContainerClient> GetContainerClientAsync(string containerName);

        /// <summary>
        /// Gets the BLOB client asynchronous.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="blobName">Name of the BLOB.</param>
        /// <returns>The blob client.</returns>
        Task<BlobClient> GetBlobClientAsync(string containerName, string blobName);
    }
}
