using Azure.Storage.Blobs;

namespace FileConvertor.Services
{
    /// <summary>
    /// This class represents the Azure storage service.
    /// </summary>
    /// <seealso cref="IAzureStorageService" />
    public class AzureStorageService(BlobServiceClient blobServiceClient) : IAzureStorageService
    {
        ///</inheritdoc>
        public async Task<BlobClient> GetBlobClientAsync(string containerName, string blobName)
        {
            var containerClient = await GetContainerClientAsync(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            return blobClient;
        }

        ///</inheritdoc>
        public Task<BlobContainerClient> GetContainerClientAsync(string containerName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName.ToLower());

            if (!containerClient.Exists())
            {
                containerClient.Create();
            }

            return Task.FromResult(containerClient);
        }

        ///</inheritdoc>
        public Task UploadFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
