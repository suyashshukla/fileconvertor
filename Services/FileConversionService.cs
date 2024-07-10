using Azure.Storage.Blobs;
using FileConvertor.Builders;
using FileConvertor.Models;

namespace FileConvertor.Services
{
    /// <summary>
    /// This class represents the file conversion service.
    /// </summary>
    /// <seealso cref="FileConvertor.Services.IFileConversionService" />
    public class FileConversionService(IAzureStorageService azureStorageService) : IFileConversionService
    {
        ///</inheritdoc>
        public async Task<IEnumerable<FileConversionResponseDto>> Convert(IEnumerable<FileConversionRequestDto> conversionRequests, ConversionType conversionType = ConversionType.FileUrl)
        {
            var containerClient = await azureStorageService.GetContainerClientAsync(Constants.FileConversionContainerName);
            var conversionResponses = new List<FileConversionResponseDto>();

            foreach (var conversionRequest in conversionRequests)
            {
                var (blobClient, blobUrl) = BuildBlobClient(containerClient, conversionRequest.FileIdentifier);
                var fileStream = SyncfusionDocumentBuilder.BuildConvertedDocumentStream(conversionRequest, conversionType);
                var sasUrl = await blobClient.UploadFileAsync(blobUrl, fileStream);
                conversionResponses.Add(FileConversionResponseDto.CreateNew(conversionRequest.FileIdentifier,
                    sasUrl));
            }

            return conversionResponses;
        }

        /// <summary>
        /// Builds the BLOB client.
        /// </summary>
        /// <param name="containerClient">The container client.</param>
        /// <param name="fileIdentifier">The file identifier.</param>
        /// <returns>
        /// The blob client and URL of the blob.
        /// </returns>
        private (BlobClient, string) BuildBlobClient(BlobContainerClient containerClient, Guid fileIdentifier)
        {
            var blobUrl = Constants.GetFileConversionBlobName(fileIdentifier);
            var blobClient = containerClient.GetBlobClient(blobUrl);

            return (blobClient, blobUrl);
        }
    }
}
